using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;

namespace RelMa.Application.UseCases.Assets.V1.Commands;

public sealed class ImportAssetCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<ImportAssetCommand, int>
{
    public async Task<int> Handle(ImportAssetCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
