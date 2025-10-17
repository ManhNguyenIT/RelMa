using ClosedXML.Excel;
using RelMa.Application.Attributes;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace RelMa.Application.Helpers;

public static class ExcelHelper
{
    private static readonly ConcurrentDictionary<Type, List<(PropertyInfo Prop, string Letter)>> columnCache = new();

    #region ======= IMPORT =======

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
        var columnProps = GetColumnProperties<T>();

        if (columnProps.Count == 0)
            yield break;

        var endRowNumber = worksheet.LastRowUsed()?.RowNumber() ?? startRow;
        var rows = worksheet.RowsUsed().Where(r => r.RowNumber() >= startRow && r.RowNumber() <= endRowNumber);
        foreach (var row in rows)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var item = new T();
            MapRowToItem(row, columnProps, item);

            yield return item;
            await Task.Yield();
        }
    }

    private static void MapRowToItem<T>(
        IXLRow row,
        IEnumerable<(PropertyInfo Prop, string Letter)> columnProps,
        T item)
    {
        foreach (var (prop, letter) in columnProps)
        {
            if (!row.Cell(letter).TryGetValue<XLCellValue>(out var value) || value.IsBlank)
                continue;

            var converted = ConvertCellValue(value, prop.PropertyType);
            if (converted != null)
                prop.SetValue(item, converted);
        }
    }

    private static object? ConvertCellValue(XLCellValue value, Type targetType)
    {
        var underlyingType = Nullable.GetUnderlyingType(targetType);
        var isNullable = underlyingType != null;
        var actualType = underlyingType ?? targetType;

        object? result = actualType switch
        {
            Type t when t == typeof(string) => value.GetText(),
            Type t when t == typeof(DateTime) => value.GetDateTime(),
            Type t when t == typeof(DateTimeOffset) => new DateTimeOffset(value.GetDateTime()),
            Type t when t == typeof(int) => Convert.ToInt32(value.GetNumber()),
            Type t when t == typeof(long) => Convert.ToInt64(value.GetNumber()),
            Type t when t == typeof(float) => Convert.ToSingle(value.GetNumber()),
            Type t when t == typeof(double) => value.GetNumber(),
            Type t when t == typeof(decimal) => Convert.ToDecimal(value.GetNumber()),
            Type t when t.IsEnum => Enum.Parse(t, value.GetText(), ignoreCase: true),
            _ => throw new NotSupportedException($"Unsupported property type: {targetType.Name}")
        };

        return isNullable ? Activator.CreateInstance(targetType, result) : result;
    }

    private static List<(PropertyInfo Prop, string Letter)> GetColumnProperties<T>()
        => columnCache.GetOrAdd(typeof(T), t =>
        {
            var props = t.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite && p.GetCustomAttribute<ColumnLetterAttribute>() is not null)
                .Select(p =>
                {
                    var attr = p.GetCustomAttribute<ColumnLetterAttribute>();
                    var letter = string.IsNullOrEmpty(attr?.Letter) ? p.Name : attr.Letter.ToUpperInvariant();
                    return (p, letter);
                })
                .ToList();
            return props;
        });

    #endregion

    #region ======= EXPORT =======

    public static async Task<MemoryStream> ExportExcelStreamAsync<T>(
        string template,
        IAsyncEnumerable<T> data,
        int startRow = 2,
        string? sheetName = default,
        CancellationToken cancellationToken = default)
    {
        using var workbook = new XLWorkbook(template);
        return await ExportAsync(workbook, sheetName, null, data, startRow, cancellationToken);
    }

    public static async Task<MemoryStream> ExportExcelStreamAsync<T>(
        string template,
        IAsyncEnumerable<T> data,
        int startRow = 2,
        int sheetIndex = 1,
        CancellationToken cancellationToken = default)
    {
        using var workbook = new XLWorkbook(template);
        return await ExportAsync(workbook, null, sheetIndex, data, startRow, cancellationToken);
    }

    public static async Task<MemoryStream> ExportExcelStreamAsync<T>(
        MemoryStream fileStream,
        IAsyncEnumerable<T> data,
        int startRow = 2,
        string? sheetName = default,
        CancellationToken cancellationToken = default)
    {
        fileStream.ResetPositionIfNeeded();

        using var workbook = new XLWorkbook(fileStream);
        return await ExportAsync(workbook, sheetName, null, data, startRow, cancellationToken);
    }

    public static async Task<MemoryStream> ExportExcelStreamAsync<T>(
        MemoryStream fileStream,
        IAsyncEnumerable<T> data,
        int startRow = 2,
        int sheetIndex = 1,
        CancellationToken cancellationToken = default)
    {
        fileStream.ResetPositionIfNeeded();

        using var workbook = new XLWorkbook(fileStream);
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

        var columnProps = GetOrCreateExportColumnProperties<T>();

        await ExportDataToSheetAsync(worksheet, data, startRow, columnProps, cancellationToken);

        var memoryStream = new MemoryStream();
        workbook.SaveAs(memoryStream, false);
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

                if (value is null)
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

    private static List<(PropertyInfo Prop, string Letter)> GetOrCreateExportColumnProperties<T>()
    {
        return columnCache.GetOrAdd(typeof(T), t =>
        {
            var props = t.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.GetCustomAttribute<ColumnLetterAttribute>() is not null)
                .Select(p =>
                {
                    var attr = p.GetCustomAttribute<ColumnLetterAttribute>();
                    var letter = string.IsNullOrEmpty(attr?.Letter) ? p.Name : attr.Letter.ToUpperInvariant();
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
        if (!string.IsNullOrWhiteSpace(sheetName))
            return workbook.Worksheet(sheetName);

        if (sheetIndex.HasValue)
            return workbook.Worksheet(sheetIndex.Value);

        return workbook.Worksheets.First();
    }

    private static void ResetPositionIfNeeded(this Stream stream)
    {
        if (stream.CanSeek && stream.Position != 0)
            stream.Position = 0;
    }

    #endregion
}