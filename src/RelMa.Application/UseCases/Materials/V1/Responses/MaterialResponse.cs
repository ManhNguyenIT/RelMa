using RelMa.Application.UseCases.Items.V1.Responses;

namespace RelMa.Application.UseCases.Materials.V1.Responses;

public class MaterialResponse
{
    public MaterialResponse()
    {
        Items = [];
    }
    public required Ulid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public int MinQty { get; set; }
    public int AvailableQty { get; set; }
    public int IncomingQty { get; set; }
    public int AllocatedQty { get; set; }
    public bool Status => MinQty <= AvailableQty + IncomingQty;
    public int OnHandQty => Items?.Sum(x => x.Quantity) ?? 0;
    public virtual IEnumerable<ItemResponse> Items { get; set; }
}
