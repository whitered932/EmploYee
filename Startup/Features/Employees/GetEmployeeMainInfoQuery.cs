using EmploYee.Core.Models;
using EmploYee.Core.Repositories;
using EmploYee.Core.Specifications;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Ftsoft.Domain.Specification;
using Startup.Features.Employees.Models;
using Startup.Features.Errors;
using Startup.Features.Meeting.Models;
using Startup.Features.Task.Models;
using Startup.Providers;

namespace Startup.Features.Employees;

public class GetEmployeeMainInfoQuery : Query<EmployeeMainInfoDto>
{
    public long? UserId { get; set; }
}

public sealed class GetEmployeeMainInfoQueryHandler(
    IEmployeeRepository employeeRepository,
    ITaskRepository taskRepository,
    IAchievementRepository achievementRepository,
    ISecurityProvider securityProvider,
    IMeetingRepository meetingRepository,
    IDepartmentRepository departmentRepository
) : QueryHandler<GetEmployeeMainInfoQuery, EmployeeMainInfoDto>
{
    public override async Task<Result<EmployeeMainInfoDto>> Handle(GetEmployeeMainInfoQuery request,
        CancellationToken cancellationToken)
    {
        var user = securityProvider.SecurityProfile.User;
        if (user.Role is UserRole.Employee && request.UserId is not null)
        {
            return Error(BadRequestError.Instance);
        }

        var userId = request.UserId ?? securityProvider.SecurityProfile.User.Id;
        var employee =
            await employeeRepository.SingleOrDefaultAsync(EmployeeSpecification.GetById(userId).IsSatisfiedBy(),
                cancellationToken);
        if (employee is null)
        {
            return Error(NotFoundError.Instance);
        }

        var achievementCount = await achievementRepository.CountAsync(cancellationToken);
        var collectedAchievementsCount = employee.AchievementHistories.Count;

        var startDate = DateTime.UtcNow.Date;
        var endDate = startDate.AddDays(1);

        var (todayMeetings, tomorrowMeetings) = await GetMeetings(employee, startDate, endDate, cancellationToken);
        var tasks = await taskRepository.Query(x =>
        {
            x = x.Where(b => b.PerformerIds.Contains(employee.Id));
            return x.OrderBy(b => b.Id);
        }, cancellationToken);
        var taskDtos = tasks.Select(x => new ItemTaskDto()
        {
            Id = x.Id,
            Title = x.Title,
            Checked = x.IsChecked()
        }).ToList();

        var progress = ((double)taskDtos.Count(x => x.Checked) + collectedAchievementsCount) /
            (achievementCount + taskDtos.Count) * 100;

        var employeeDto = new EmployeeMainInfoDto()
        {
            ProgressPercentage = (int)progress,
            UcoinsCount = employee.Balance,
            AllAchievementsCount = achievementCount,
            CurrentAchievementsCount = collectedAchievementsCount,
            Todo = taskDtos,
            TommorowMeetings = tomorrowMeetings,
            TodayMeetings = todayMeetings,
        };
        return Successful(employeeDto);
    }

    private async Task<(List<GetMeetingDto> todayMeetings, List<GetMeetingDto> tomorrowMeetings)> GetMeetings(
        Employee employee, DateTime startDate,
        DateTime endDate, CancellationToken cancellationToken)
    {
        var meetings =
            await meetingRepository.ListAsync(
                MeetingSpecification.GetByParticipantId(employee.Id, startDate, endDate.Date.AddDays(1)),
                cancellationToken);

        var todayMeetings = meetings.Where(x => startDate < x.Date && x.Date < startDate.AddDays(1)).Select(x =>
            new GetMeetingDto()
            {
                Title = x.Title,
                Description = x.Description,
                Id = x.Id,
                Date = new DateTimeOffset(x.Date).ToUnixTimeMilliseconds()
            }).ToList();
        var tomorrowMeetings = meetings.Where(x => endDate < x.Date && x.Date < endDate.AddDays(2)).Select(x =>
            new GetMeetingDto()
            {
                Title = x.Title,
                Description = x.Description,
                Id = x.Id,
                Date = new DateTimeOffset(x.Date).ToUnixTimeMilliseconds()
            }).ToList();
        return (todayMeetings, tomorrowMeetings);
    }
}