using ClosedXML.Excel;
using RelMa.Application.Attributes;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace RelMa.Application.Helpers;

public static class ExcelHelper
{
    // Separate caches for import (writeable props) and export (readable props) to avoid conflicts
    private static readonly ConcurrentDictionary<string, List<(PropertyInfo Prop, string Letter)>> importColumnCache = new();
    private static readonly ConcurrentDictionary<string, List<(PropertyInfo Prop, string Letter)>> exportColumnCache = new();

    #region ======= IMPORT =======

    /// <summary>
    /// Imports data from Excel stream asynchronously.
    /// </summary>
    public static async IAsyncEnumerable<T> ImportExcelAsync<T>(
        Stream fileStream,
        int startRow = 2,
        string? sheetName = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
        where T : new()
    {
        fileStream.ResetPositionIfNeeded();

        using var workbook = new XLWorkbook(fileStream);
        await foreach (var item in ImportAsync<T>(workbook, startRow, sheetName, null, cancellationToken))
        {
            yield return item;
        }
    }

    /// <summary>
    /// Imports data from Excel stream asynchronously using sheet index.
    /// </summary>
    public static async IAsyncEnumerable<T> ImportExcelAsync<T>(
        Stream fileStream,
        int startRow = 2,
        int sheetIndex = 1,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
        where T : new()
    {
        fileStream.ResetPositionIfNeeded();

        using var workbook = new XLWorkbook(fileStream);
        await foreach (var item in ImportAsync<T>(workbook, startRow, null, sheetIndex, cancellationToken))
        {
            yield return item;
        }
    }

    private static async IAsyncEnumerable<T> ImportAsync<T>(
        XLWorkbook workbook,
        int startRow = 2,
        string? sheetName = default,
        int? sheetIndex = 1,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
        where T : new()
    {
        var worksheet = GetWorksheet(workbook, sheetName, sheetIndex);
        var columnProps = GetImportColumnProperties<T>();

        if (columnProps.Count == 0)
            yield break;

        var lastRowNumber = worksheet.LastRowUsed()?.RowNumber() ?? (startRow - 1);

        // Iterate row by row without loading all rows into memory
        for (int rowNum = startRow; rowNum <= lastRowNumber; rowNum++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var row = worksheet.Row(rowNum);
            if (row.IsEmpty())
                continue;

            var item = new T();
            MapRowToItem(row, columnProps, item);

            // Only yield if the row has some data mapped
            if (HasMappedData(item, columnProps))
            {
                yield return item;
            }

            await Task.Yield(); // Yield control for async streaming
        }
    }

    private static void MapRowToItem<T>(
        IXLRow row,
        IEnumerable<(PropertyInfo Prop, string Letter)> columnProps,
        T item)
    {
        foreach (var (prop, letter) in columnProps)
        {
            var cell = row.Cell(letter);
            if (cell.IsEmpty())
                continue;

            var cellValue = cell.Value;
            var converted = ConvertCellValue(cellValue, prop.PropertyType);
            if (converted != null)
                prop.SetValue(item, converted);
        }
    }

    /// <summary>
    /// Converts XLCellValue to the target property type with improved handling for common types.
    /// </summary>
    private static object? ConvertCellValue(XLCellValue value, Type targetType)
    {
        if (value.IsBlank)
            return null;

        var underlyingType = Nullable.GetUnderlyingType(targetType);
        var isNullable = underlyingType != null;
        var actualType = underlyingType ?? targetType;

        object? result;
        try
        {
            result = actualType switch
            {
                Type t when t == typeof(string) => value.GetText(),
                Type t when t == typeof(DateTime) => value.GetDateTime(),
                Type t when t == typeof(DateTimeOffset) => new DateTimeOffset(value.GetDateTime()),
                Type t when t == typeof(int) => (int)value.GetNumber(),
                Type t when t == typeof(long) => (long)value.GetNumber(),
                Type t when t == typeof(float) => (float)value.GetNumber(),
                Type t when t == typeof(double) => value.GetNumber(),
                Type t when t == typeof(decimal) => (decimal)value.GetNumber(),
                Type t when t == typeof(bool) => value.GetBoolean(),
                Type t when t.IsEnum => Enum.TryParse(actualType, value.GetText(), true, out var enumValue) ? enumValue : null,
                _ => value.GetText() // Fallback to string for unsupported types
            };
        }
        catch
        {
            result = null; // Silently handle conversion failures
        }

        if (result == null && !isNullable)
            return Activator.CreateInstance(actualType); // Default for non-nullable

        return isNullable ? (result == null ? null : Activator.CreateInstance(targetType, result)) : result;
    }

    /// <summary>
    /// Checks if any property was mapped to avoid yielding empty objects.
    /// </summary>
    private static bool HasMappedData<T>(T item, IEnumerable<(PropertyInfo Prop, string Letter)> columnProps)
    {
        foreach (var (prop, _) in columnProps)
        {
            var propValue = prop.GetValue(item);
            var defaultValue = Activator.CreateInstance(prop.PropertyType);
            if (!Equals(propValue, defaultValue))
            {
                return true;
            }
        }
        return false;
    }

    private static List<(PropertyInfo Prop, string Letter)> GetImportColumnProperties<T>()
    {
        var cacheKey = $"import:{typeof(T).FullName}";
        return importColumnCache.GetOrAdd(cacheKey, key =>
        {
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite && p.GetCustomAttribute<ColumnLetterAttribute>() is not null)
                .Select(p =>
                {
                    var attr = p.GetCustomAttribute<ColumnLetterAttribute>()!;
                    var letter = string.IsNullOrEmpty(attr.Letter) ? p.Name : attr.Letter.ToUpperInvariant();
                    return (p, letter);
                })
                .ToList();
            return props;
        });
    }

    #endregion

    #region ======= EXPORT =======

    /// <summary>
    /// Exports async enumerable data to Excel stream from template file.
    /// </summary>
    public static async Task<MemoryStream> ExportExcelStreamAsync<T>(
        string templatePath,
        IAsyncEnumerable<T> data,
        int startRow = 2,
        string? sheetName = default,
        CancellationToken cancellationToken = default)
    {
        using var workbook = new XLWorkbook(templatePath);
        return await ExportAsync(workbook, sheetName, null, data, startRow, cancellationToken);
    }

    /// <summary>
    /// Exports async enumerable data to Excel stream from template file using sheet index.
    /// </summary>
    public static async Task<MemoryStream> ExportExcelStreamAsync<T>(
        string templatePath,
        IAsyncEnumerable<T> data,
        int startRow = 2,
        int sheetIndex = 1,
        CancellationToken cancellationToken = default)
    {
        using var workbook = new XLWorkbook(templatePath);
        return await ExportAsync(workbook, null, sheetIndex, data, startRow, cancellationToken);
    }

    /// <summary>
    /// Exports async enumerable data to Excel stream from memory stream template.
    /// </summary>
    public static async Task<MemoryStream> ExportExcelStreamAsync<T>(
        MemoryStream templateStream,
        IAsyncEnumerable<T> data,
        int startRow = 2,
        string? sheetName = default,
        CancellationToken cancellationToken = default)
    {
        templateStream.ResetPositionIfNeeded();
        using var workbook = new XLWorkbook(templateStream);
        return await ExportAsync(workbook, sheetName, null, data, startRow, cancellationToken);
    }

    /// <summary>
    /// Exports async enumerable data to Excel stream from memory stream template using sheet index.
    /// </summary>
    public static async Task<MemoryStream> ExportExcelStreamAsync<T>(
        MemoryStream templateStream,
        IAsyncEnumerable<T> data,
        int startRow = 2,
        int sheetIndex = 1,
        CancellationToken cancellationToken = default)
    {
        templateStream.ResetPositionIfNeeded();
        using var workbook = new XLWorkbook(templateStream);
        return await ExportAsync(workbook, null, sheetIndex, data, startRow, cancellationToken);
    }

    private static async Task<MemoryStream> ExportAsync<T>(
        XLWorkbook workbook,
        string? sheetName,
        int? sheetIndex,
        IAsyncEnumerable<T> data,
        int startRow,
        CancellationToken cancellationToken)
    {
        var worksheet = GetWorksheet(workbook, sheetName, sheetIndex);
        var columnProps = GetExportColumnProperties<T>();

        await ExportDataToSheetAsync(worksheet, data, startRow, columnProps, cancellationToken);

        var memoryStream = new MemoryStream();
        workbook.SaveAs(memoryStream);
        memoryStream.Position = 0;
        return memoryStream;
    }

    private static async Task ExportDataToSheetAsync<T>(
        IXLWorksheet worksheet,
        IAsyncEnumerable<T> data,
        int startRow,
        List<(PropertyInfo Prop, string Letter)> columnProps,
        CancellationToken cancellationToken)
    {
        int rowIndex = startRow;

        await foreach (var item in data.WithCancellation(cancellationToken))
        {
            var row = worksheet.Row(rowIndex);

            foreach (var (prop, letter) in columnProps)
            {
                var value = prop.GetValue(item);
                var cell = row.Cell(letter);

                if (value == null)
                {
                    cell.Clear();
                    continue;
                }

                cell.Value = value switch
                {
                    string s => s,
                    int i => i,
                    long l => l,
                    float f => f,
                    double d => d,
                    decimal m => m,
                    bool b => b,
                    DateTime dt => dt,
                    DateTimeOffset dto => dto.UtcDateTime,
                    _ => value.ToString() ?? string.Empty
                };
            }

            rowIndex++;
        }
    }

    private static List<(PropertyInfo Prop, string Letter)> GetExportColumnProperties<T>()
    {
        var cacheKey = $"export:{typeof(T).FullName}";
        return exportColumnCache.GetOrAdd(cacheKey, key =>
        {
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.GetCustomAttribute<ColumnLetterAttribute>() is not null)
                .Select(p =>
                {
                    var attr = p.GetCustomAttribute<ColumnLetterAttribute>()!;
                    var letter = string.IsNullOrEmpty(attr.Letter) ? p.Name : attr.Letter.ToUpperInvariant();
                    return (p, letter);
                })
                .ToList();
            return props;
        });
    }

    #endregion

    #region ======= COMMON =======

    private static IXLWorksheet GetWorksheet(XLWorkbook workbook, string? sheetName, int? sheetIndex)
    {
        return !string.IsNullOrWhiteSpace(sheetName)
            ? workbook.Worksheet(sheetName)
            : sheetIndex.HasValue
                ? workbook.Worksheet(sheetIndex.Value)
                : workbook.Worksheets.FirstOrDefault() ?? throw new InvalidOperationException("No worksheets found.");
    }

    private static void ResetPositionIfNeeded(this Stream stream)
    {
        if (stream.CanSeek && stream.Position != 0)
            stream.Position = 0;
    }

    #endregion
}