using EmploYee.Core.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Achievement.Models;
using Startup.Features.Employees.Models;

namespace Startup.Features.Achievement;

public class GetAchievementsQuery : Query<IReadOnlyList<AchievementDto>>
{
}

public sealed class GetAchievementsQueryHandler
    (IAchievementRepository achievementRepository) : QueryHandler<GetAchievementsQuery, IReadOnlyList<AchievementDto>>
{
    public override async Task<Result<IReadOnlyList<AchievementDto>>> Handle(GetAchievementsQuery request,
        CancellationToken cancellationToken)
    {
        var achievements = await achievementRepository.ListAsync(cancellationToken);
        var achievementDtos = achievements.Select(achievement => new AchievementDto()
        {
            Id = achievement.Id,
            Title =  achievement.Title,
            Image = achievement.Image,
            CurrentValue = achievement.CurrentValue
        }).ToList();
        return Successful(achievementDtos);
    }
}