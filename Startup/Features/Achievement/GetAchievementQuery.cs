using EmploYee.Core.Repositories;
using EmploYee.Core.Specifications;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Achievement.Models;
using Startup.Features.Employees.Models;
using Startup.Features.Errors;

namespace Startup.Features.Achievement;

public class GetAchievementQuery : Query<AchievementDto>
{
    public long Id { get; set; }
}

public sealed class GetAchievementQueryHandler
    (IAchievementRepository achievementRepository) : QueryHandler<GetAchievementQuery, AchievementDto>
{
    public override async Task<Result<AchievementDto>> Handle(GetAchievementQuery request,
        CancellationToken cancellationToken)
    {
        var achievement =
            await achievementRepository.SingleOrDefaultAsync(AchievementSpecification.GetById(request.Id).IsSatisfiedBy(),
                cancellationToken);
        if (achievement is null)
        {
            return Error(NotFoundError.Instance);
        }
        var achievementDto = new AchievementDto()
        {
            Id = achievement.Id,
            Title =  achievement.Title,
            Image = achievement.Image,
            CurrentValue = achievement.CurrentValue
        };
        return Successful(achievementDto);
    }
}