using EmploYee.Core.Repositories;
using EmploYee.Core.Specifications;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Ftsoft.Domain.Specification;
using Startup.Features.Achievement.Models;
using Startup.Features.Employees.Models;
using Startup.Providers;

namespace Startup.Features.Achievement;

public class GetAchievementsQuery : Query<IReadOnlyList<AchievementDto>>
{
}

public sealed class GetAchievementsQueryHandler
    (IAchievementRepository achievementRepository, IEmployeeRepository employeeRepository, ISecurityProvider securityProvider) : QueryHandler<GetAchievementsQuery, IReadOnlyList<AchievementDto>>
{
    public override async Task<Result<IReadOnlyList<AchievementDto>>> Handle(GetAchievementsQuery request,
        CancellationToken cancellationToken)
    {
        var securityUser = securityProvider.SecurityProfile.User;
        var user = await employeeRepository.SingleOrDefaultAsync(EmployeeSpecification.GetById(securityUser.Id).IsSatisfiedBy(), cancellationToken);
        var achievements = await achievementRepository.ListAsync(cancellationToken);
        var userAchievementIds = user.AchievementHistories.Select(achievement => (long)achievement.AchievementId).ToList();
        var userAchievements = user.AchievementHistories.Select(achievement => new AchievementDto()
        {
            Id = (int)achievement.AchievementId,
            Description = "",
            IsCompleted = true,
            CurrentValue = achievement.CurrentValue,
            Img = "",
            Title = achievement.Title
        }).ToList();
        var achievementDtos = achievements.Where(x => !userAchievementIds.Contains(x.Id)).Select(achievement => new AchievementDto()
        {
            Id = achievement.Id,
            Title =  achievement.Title,
            Img = achievement.Image,
            CurrentValue = achievement.CurrentValue,
            Description = "",
            IsCompleted = false,
        }).ToList();
        userAchievements.AddRange(achievementDtos);
        return Successful(userAchievements);
    }
}