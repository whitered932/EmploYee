using EmploYee.Core.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Stage.Models;

namespace Startup.Features.Stage;

public class GetStagesQuery : Query<IReadOnlyList<StageDto>>
{
}

public sealed class GetStagesQueryHandler
    (IStageRepository stageRepository) : QueryHandler<GetStagesQuery, IReadOnlyList<StageDto>>
{
    public override async Task<Result<IReadOnlyList<StageDto>>> Handle(GetStagesQuery request,
        CancellationToken cancellationToken)
    {
        var stages = await stageRepository.ListAsync(cancellationToken);
        var stagesDtos = stages.Select(x => new StageDto()
        {
            Description = x.Description,
            Title = x.Title,
            ParentStageId = x.ParentStageId
        }).ToList();
        return Successful(stagesDtos);
    }
}