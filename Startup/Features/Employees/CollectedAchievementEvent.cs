using EmploYee.Core.Repositories;
using EmploYee.Core.Specifications;
using Ftsoft.Domain.Specification;
using MediatR;

namespace Startup.Features.Employees;

public class CollectedAchievementEvent : INotification
{
    public long UserId { get; set; }
    public long AchievementId { get; set; }
}

public sealed class CollectedAchievementEventHandler(IEmployeeRepository employeeRepository,
    IAchievementRepository achievementRepository) : INotificationHandler<CollectedAchievementEvent>
{
    public async System.Threading.Tasks.Task Handle(CollectedAchievementEvent notification,
        CancellationToken cancellationToken)
    {
        var employee =
            await employeeRepository.SingleOrDefaultAsync(
                EmployeeSpecification.GetById(notification.UserId).IsSatisfiedBy(), cancellationToken);
        if (employee is null)
        {
            return;
        }

        var achievement = await achievementRepository.SingleOrDefaultAsync(
            AchievementSpecification.GetById(notification.AchievementId).IsSatisfiedBy(), cancellationToken);
        if (achievement is null)
        {
            return;
        }

        var achievementHistory = new EmploYee.Core.Models.EmployeeAchievementHistory(achievement.Title,
            achievement.Image, achievement.CurrentValue, achievement.Id);
        employee.AddAchievement(achievementHistory);
        await employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}