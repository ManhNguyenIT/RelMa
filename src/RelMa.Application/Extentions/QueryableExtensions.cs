using RelMa.Shared.Exceptions;
using System.Globalization;
using System.Linq.Dynamic.Core;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace RelMa.Application.Extentions;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> queryable, string? filters)
    {
        if (string.IsNullOrWhiteSpace(filters))
            return queryable;

        var filterQueries = ParseFilters(filters);
        foreach (var (expression, parameters) in filterQueries)
        {
            queryable = queryable.Where(expression, parameters);
        }

        return queryable;
    }

    public static IQueryable<T> ApplySorts<T>(this IQueryable<T> queryable, string? orders)
    {
        if (string.IsNullOrEmpty(orders))
            return queryable;

        var sorts = JsonSerializer.Deserialize<Dictionary<string, string>>(orders) ?? new Dictionary<string, string>();
        var sortExpressions = new List<string>();

        foreach (var sort in sorts)
        {
            if (!IsValidFieldName(sort.Key))
                throw new BadRequestException($"Tên field '{sort.Key}' không hợp lệ");

            var direction = sort.Value.ToLower(CultureInfo.CurrentCulture);
            if (direction != "asc" && direction != "desc")
                throw new BadRequestException($"Giá trị sắp xếp không hợp lệ cho field '{sort.Key}'");

            var linqDirection = direction == "desc" ? "descending" : "ascending";
            sortExpressions.Add($"{sort.Key} {linqDirection}");
        }

        return sortExpressions.Count != 0
            ? queryable.OrderBy(string.Join(", ", sortExpressions))
            : queryable;
    }

    private static List<(string Expression, object[] Parameters)> ParseFilters(string filtersJson)
    {
        var filterQueries = new List<(string Expression, object[] Parameters)>();
        var filterArray = JsonNode.Parse(filtersJson)?.AsArray();
        if (filterArray == null) return filterQueries;

        if (filterArray.Count > 10)
            throw new BadRequestException("Số lượng filter vượt quá giới hạn cho phép");

        foreach (var filter in filterArray)
        {
            if (filter == null) continue;

            var field = filter["field"]?.GetValue<string>()?.Trim() ?? string.Empty;
            var op = filter["op"]?.GetValue<string>()?.ToLower(CultureInfo.CurrentCulture);
            var value = filter["value"];

            if (!IsValidFieldName(field))
                throw new BadRequestException($"Tên field '{field}' không hợp lệ");

            if (string.IsNullOrWhiteSpace(op))
                throw new BadRequestException("Operator không hợp lệ");

            if (!IsValidFilterValue(op, value))
                throw new BadRequestException($"Giá trị không hợp lệ cho operator '{op}' và field '{field}'");

            var (expression, parameters) = CreateFilterExpression(field, op, value);
            filterQueries.Add((expression, parameters));
        }

        return filterQueries;
    }

    private static bool IsValidFieldName(string field)
    {
        if (string.IsNullOrWhiteSpace(field))
            return false;

        var invalidFieldPattern = @"[^a-zA-Z0-9._]";
        return !Regex.IsMatch(field, invalidFieldPattern);
    }

    private static bool IsValidFilterValue(string op, JsonNode? value)
    {
        if (value is null || value.GetValueKind() == JsonValueKind.Null)
            return op is "eq" or "neq";

        return op switch
        {
            "eq" or "neq" => value.GetValueKind() is JsonValueKind.String or JsonValueKind.Array or JsonValueKind.Number or JsonValueKind.True or JsonValueKind.False,
            "in" or "not_in" => value is JsonArray,
            "gt" or "gte" or "lt" or "lte" => value.GetValueKind() is JsonValueKind.Number or JsonValueKind.String,
            "contains" or "startswith" or "endswith" or "like" => value.GetValueKind() is JsonValueKind.String,
            _ => false
        };
    }

    private enum RangeOperator { Gt, Gte, Lt, Lte }
    private enum StringOperator { Contains, StartsWith, EndsWith, Like }

    private static (string Expression, object[] Parameters) CreateFilterExpression(string field, string op, JsonNode? value)
        => op switch
        {
            "eq" => CreateEqFilter(field, value),
            "neq" => CreateNeqFilter(field, value),
            "in" => CreateInFilter(field, value),
            "not_in" => CreateNotInFilter(field, value),
            "gt" => CreateRangeFilter(field, value, RangeOperator.Gt),
            "gte" => CreateRangeFilter(field, value, RangeOperator.Gte),
            "lt" => CreateRangeFilter(field, value, RangeOperator.Lt),
            "lte" => CreateRangeFilter(field, value, RangeOperator.Lte),
            "contains" => CreateStringFilter(field, value, StringOperator.Contains),
            "startswith" => CreateStringFilter(field, value, StringOperator.StartsWith),
            "endswith" => CreateStringFilter(field, value, StringOperator.EndsWith),
            "like" => CreateStringFilter(field, value, StringOperator.Like),
            _ => throw new BadRequestException($"Operator '{op}' không được hỗ trợ")
        };

    private static (string Expression, object[] Parameters) CreateEqFilter(string field, JsonNode? node)
    {
        if (node is null || node.GetValueKind() == JsonValueKind.Null)
            return ($"{field} == null", []);

        if (node is JsonArray arr)
        {
            var values = FormatArrayValues(arr, field);
            return ($"{field} in ({string.Join(", ", values)})", []);
        }

        var (expression, parameters) = FormatValue(node, field);
        return ($"{field} == {expression}", parameters);
    }

    private static (string Expression, object[] Parameters) CreateNeqFilter(string field, JsonNode? node)
    {
        if (node is null || node.GetValueKind() == JsonValueKind.Null)
            return ($"{field} != null", []);

        if (node is JsonArray arr)
        {
            var values = FormatArrayValues(arr, field);
            return ($"{field} not in ({string.Join(", ", values)})", []);
        }

        var (expression, parameters) = FormatValue(node, field);
        return ($"{field} != {expression}", parameters);
    }

    private static (string Expression, object[] Parameters) CreateInFilter(string field, JsonNode? node)
    {
        if (node is not JsonArray arr)
            throw new BadRequestException($"Giá trị phải là một mảng cho operator 'in' và field '{field}'");

        var values = FormatArrayValues(arr, field);
        return ($"{field} in ({string.Join(", ", values)})", []);
    }

    private static (string Expression, object[] Parameters) CreateNotInFilter(string field, JsonNode? node)
    {
        if (node is not JsonArray arr)
            throw new BadRequestException($"Giá trị phải là một mảng cho operator 'not_in' và field '{field}'");

        var values = FormatArrayValues(arr, field);
        return ($"{field} not in ({string.Join(", ", values)})", []);
    }

    private static (string Expression, object[] Parameters) CreateRangeFilter(string field, JsonNode? node, RangeOperator rangeOp)
    {
        if (node is null || node.GetValueKind() == JsonValueKind.Null)
            throw new BadRequestException($"Giá trị không hợp lệ cho operator '{rangeOp}' và field '{field}'");

        var opSymbol = rangeOp switch
        {
            RangeOperator.Gt => ">",
            RangeOperator.Gte => ">=",
            RangeOperator.Lt => "<",
            RangeOperator.Lte => "<=",
            _ => throw new InvalidOperationException("Invalid range operator")
        };

        return node.GetValueKind() switch
        {
            JsonValueKind.Number => CreateNumberRangeFilter(field, node, opSymbol),
            JsonValueKind.String => CreateDateRangeFilter(field, node, opSymbol),
            _ => throw new BadRequestException($"Kiểu dữ liệu không hợp lệ cho operator '{rangeOp}' và field '{field}'")
        };
    }

    private static (string Expression, object[] Parameters) CreateNumberRangeFilter(string field, JsonNode node, string opSymbol)
    {
        var value = node.AsValue();
        if (value.TryGetValue<long>(out var longVal))
            return ($"{field} {opSymbol} {longVal}", []);
        if (value.TryGetValue<double>(out var doubleVal))
            return ($"{field} {opSymbol} {doubleVal}", []);

        throw new BadRequestException($"Giá trị số không hợp lệ cho field '{field}'");
    }

    private static (string Expression, object[] Parameters) CreateDateRangeFilter(string field, JsonNode node, string opSymbol)
    {
        var value = node.GetValue<string>();
        if (string.IsNullOrWhiteSpace(value))
            throw new BadRequestException($"Giá trị ngày không hợp lệ cho field '{field}'");

        if (DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var dateVal))
            return ($"{field} {opSymbol} @0", new object[] { dateVal });

        throw new BadRequestException($"Giá trị ngày không hợp lệ cho field '{field}'");
    }

    private static (string Expression, object[] Parameters) CreateStringFilter(string field, JsonNode? node, StringOperator stringOp)
    {
        if (node is null || node.GetValueKind() != JsonValueKind.String)
            throw new BadRequestException($"Giá trị phải là chuỗi cho operator '{stringOp}' và field '{field}'");

        var value = node.GetValue<string>();
        if (string.IsNullOrWhiteSpace(value))
            throw new BadRequestException($"Giá trị chuỗi không hợp lệ cho field '{field}'");

        if (stringOp == StringOperator.Like)
        {
            if (value.StartsWith('%') && value.EndsWith('%'))
                return ($"{field}.Contains(@0)", new object[] { value.Trim('%') });
            if (value.StartsWith('%'))
                return ($"{field}.EndsWith(@0)", new object[] { value.TrimStart('%') });
            if (value.EndsWith('%'))
                return ($"{field}.StartsWith(@0)", new object[] { value.TrimEnd('%') });
            return ($"{field}.Contains(@0)", new object[] { value });
        }

        var method = stringOp switch
        {
            StringOperator.Contains => "Contains",
            StringOperator.StartsWith => "StartsWith",
            StringOperator.EndsWith => "EndsWith",
            _ => throw new InvalidOperationException("Invalid string operator")
        };

        return ($"{field}.{method}(@0)", new object[] { value });
    }

    private static List<string> FormatArrayValues(JsonArray arr, string field)
    {
        var values = arr
            .Select(x => FormatValue(x, field))
            .Where(x => x.Expression != null)
            .Select(x => x.Expression!)
            .ToList();

        if (values.Count > 50)
            throw new BadRequestException("Số lượng phần tử vượt quá giới hạn cho phép");

        if (values.Count == 0)
            throw new BadRequestException($"Mảng giá trị rỗng không hợp lệ cho field '{field}'");

        return values;
    }

    private static (string? Expression, object[] Parameters) FormatValue(JsonNode? node, string field)
    {
        if (node is null)
            return (null, []);

        return node.GetValueKind() switch
        {
            JsonValueKind.String => ($"\"{node.GetValue<string>()}\"", []),
            JsonValueKind.Number => node.AsValue().TryGetValue(out long longVal)
                ? (longVal.ToString(CultureInfo.InvariantCulture), [])
                : (node.GetValue<double>().ToString(CultureInfo.InvariantCulture), []),
            JsonValueKind.True => ("true", []),
            JsonValueKind.False => ("false", []),
            _ => throw new BadRequestException($"Kiểu dữ liệu không được hỗ trợ cho field '{field}'")
        };
    }
}
