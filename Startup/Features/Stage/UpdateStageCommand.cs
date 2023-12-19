using EmploYee.Core.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Errors;

namespace Startup.Features.Stage;

public class UpdateStageCommand : Command
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}

public sealed class UpdateStageCommandHandler(IStageRepository stageRepository) : CommandHandler<UpdateStageCommand>
{
    public override async Task<Result> Handle(UpdateStageCommand request, CancellationToken cancellationToken)
    {
        var stage = await stageRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (stage is null)
        {
            return Error(BadRequestError.Instance);
        }

        stage.Update(request.Title, request.Description);
        await stageRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}