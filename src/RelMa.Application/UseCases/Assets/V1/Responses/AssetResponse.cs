using RelMa.Application.Attributes;
using RelMa.Application.UseCases.Locations.V1.Responses;
using System.Text.Json.Serialization;

namespace RelMa.Application.UseCases.Assets.V1.Responses;

public class AssetResponse
{
    [ColumnLetter("A")]
    public required DefaultIdType Id { get; set; }
    [ColumnLetter("B")]
    public required string Name { get; set; }
    [ColumnLetter("C")]
    public string? Area { get; set; }
    [JsonIgnore]
    [ColumnLetter("D")]
    public string? LocationName => Location?.Name;
    [ColumnLetter("E")]
    public string? Code { get; set; }
    [ColumnLetter("F")]
    public string? Category { get; set; }
    [ColumnLetter("G")]
    public string? Description { get; set; }
    [ColumnLetter("H")]
    public string? Model { get; set; }
    [ColumnLetter("I")]
    public string? SerialNumber { get; set; }
    public required DefaultIdType LocationId { get; set; }
    public virtual LocationResponse? Location { get; set; }
}
