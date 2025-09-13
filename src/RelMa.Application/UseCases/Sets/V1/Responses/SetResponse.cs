using RelMa.Application.UseCases.Parts.V1.Responses;

namespace RelMa.Application.UseCases.Sets.V1.Responses;

public class SetResponse
{
    public SetResponse()
    {
        Parts = [];
    }
    public required DefaultIdType Id { get; set; }
    public required string Name { get; set; }
    public virtual IEnumerable<PartResponse> Parts { get; set; }
}
