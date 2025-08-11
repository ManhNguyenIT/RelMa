using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Globalization;

namespace RelMa.Infrastructure.Converters;
public class UlidToStringConverter : ValueConverter<Ulid, string>
{
    private static readonly ConverterMappingHints defaultHints = new(size: 26);

    public UlidToStringConverter() : this(null)
    {
    }

    public UlidToStringConverter(ConverterMappingHints? mappingHints)
        : base(
                convertToProviderExpression: x => x.ToString(),
                convertFromProviderExpression: x => Ulid.Parse(x, CultureInfo.InvariantCulture),
                mappingHints: defaultHints.With(mappingHints))
    {
    }
}
