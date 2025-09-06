using RelMa.Application.UseCases.Items.V1.Responses;

namespace RelMa.Application.UseCases.Parts.V1.Responses;

public class PartResponse
{
    public PartResponse()
    {
        Items = [];
    }
    public required Ulid Id { get; set; }
    public required string Name { get; set; }
    public required string PartNumber { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public int Quantity { get; set; }
    public decimal Cost { get; set; }
    public virtual IEnumerable<ItemResponse> Items { get; set; }
}
