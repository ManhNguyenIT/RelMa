using NPOI.XSSF.UserModel;
using RelMa.Application.Attributes;
using System.Reflection;

namespace RelMa.Application.Helpers;

public static class ExcelHelper
{
    public static async Task<MemoryStream> ExportExcelStreamAsync<T>(string template, IAsyncEnumerable<T> data, int startRow = 2, CancellationToken cancellationToken = default)
    {
        var memoryStream = new MemoryStream();
        var properties = typeof(T).GetProperties();

        using (var fileStream = new FileStream(template, FileMode.Open, FileAccess.Read))
        {
            using var workbook = new XSSFWorkbook(fileStream);
            var worksheet = workbook.GetSheetAt(0);

            int currentRow = startRow;
            await foreach (var item in data.WithCancellation(cancellationToken))
            {
                var row = worksheet.GetRow(currentRow) ?? worksheet.CreateRow(currentRow);
                for (int i = 0; i < properties.Length; i++)
                {
                    var columnAttribute = properties[i].GetCustomAttribute<ColumnLetterAttribute>();
                    if (columnAttribute is null) continue;

                    string columnName = string.IsNullOrEmpty(columnAttribute.Letter) ? properties[i].Name : columnAttribute.Letter.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
                    int columnIndex = GetColumnIndex(columnName);

                    var cell = row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);
                    var value = properties[i].GetValue(item) ?? DBNull.Value;

                    if (value != DBNull.Value)
                    {
                        if (value is int intValue)
                            cell.SetCellValue(intValue);
                        else if (value is double doubleValue)
                            cell.SetCellValue(doubleValue);
                        else if (value is DateTime dateValue)
                            cell.SetCellValue(dateValue);
                        else
                            cell.SetCellValue(value.ToString());
                    }
                }
                currentRow++;
            }

            workbook.Write(memoryStream, leaveOpen: true);
        }

        memoryStream.Position = 0;
        return memoryStream;
    }

    private static int GetColumnIndex(string columnName)
    {
        int columnIndex = 0;
        for (int i = 0; i < columnName.Length; i++)
        {
            columnIndex *= 26;
            columnIndex += columnName[i] - 'A';
        }
        return columnIndex;
    }
}