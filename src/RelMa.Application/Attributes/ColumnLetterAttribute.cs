namespace RelMa.Application.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class ColumnLetterAttribute(string letter) : Attribute
{
    public string Letter { get; } = letter;
}
