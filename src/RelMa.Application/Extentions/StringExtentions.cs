using System.Text.RegularExpressions;

namespace RelMa.Application.Extentions;

public static class StringExtentions
{
    public static string ToSnakeCase(this string str) 
        => Regex.Replace(
            Regex.Replace(
                str,
                @"([a-z0-9])([A-Z])",
                "$1_$2"
            ),
            @"\s+",
            "_"
        ).ToLower(System.Globalization.CultureInfo.CurrentCulture);
}
