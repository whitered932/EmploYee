using EmploYee.Core.Repositories;
using EmploYee.Core.Specifications;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;

namespace Startup.Features.Stage;

public class CreateStageCommand : Command
{
    public string Title { get; set; }
    public long? ParentStageId { get; set; }
    public string Description { get; set; }
}

public sealed class CreateStageCommandHandler(IStageRepository stageRepository) : CommandHandler<CreateStageCommand>
{
    public override async Task<Result> Handle(CreateStageCommand request, CancellationToken cancellationToken)
    {
        var stage = new EmploYee.Core.Models.Stage(request.Title, request.ParentStageId, request.Description);
        await stageRepository.AddAsync(stage, cancellationToken);
        await stageRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}