using RelMa.Application.UseCases.Parts.V1.Responses;
using RelMa.Domain.Materials;

namespace RelMa.Application.UseCases.Materials.V1.Responses;

public class MaterialResponse
{
    public MaterialResponse()
    {
        Parts = [];
    }
    public required DefaultIdType Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public string? Description { get; set; }
    public string[]? Images { get; set; }
    public Status Status { get; set; }
    public int Available { get; set; }
    public int Allocated { get; set; }
    public int OnHand { get; set; }
    public int Incoming { get; set; }
    public int Minimum { get; set; }
    public virtual IEnumerable<PartResponse> Parts { get; set; }
}
