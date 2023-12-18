﻿using EmploYee.Core.Models;
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
using TaskStatus = EmploYee.Core.Models.TaskStatus;

namespace Startup.Features.Employees;

public class GetEmployeeMainInfoQuery : Query<EmployeeMainInfoDto>
{
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
        var employee =
            await employeeRepository.SingleOrDefaultAsync(EmployeeSpecification.GetById(user.Id).IsSatisfiedBy(),
                cancellationToken);
        if (employee is null)
        {
            return Error(NotFoundError.Instance);
        }

        var achievementCount = await achievementRepository.CountAsync(cancellationToken);
        var collectedAchievementsCount = employee.AchievementHistories.Count;

        var startDate = DateTime.UtcNow.Date;
        var endDate = startDate.AddDays(2);

        var (todayMeetings, tomorrowMeetings) = await GetMeetings(employee, startDate, endDate, cancellationToken);
        var tasks = await taskRepository.ListAsync(x => x.PerformerIds.Contains(user.Id), cancellationToken);
        var taskDtos = tasks.Select(x => new ItemTaskDto()
        {
            Id = x.Id,
            Title = x.Title,
            Checked = x.IsChecked()
        }).ToList();
        var employeeDto = new EmployeeMainInfoDto()
        {
            ProgressPercentage = 100,
            UcoinsCount = 10,
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
            await meetingRepository.ListAsync(MeetingSpecification.GetByParticipantId(employee.Id, startDate, endDate),
                cancellationToken);

        var todayMeetings = meetings.Where(x => startDate < x.Date && x.Date < startDate.AddDays(1)).Select(x =>
            new GetMeetingDto()
            {
                Title = x.Title,
                Description = x.Description,
                Id = x.Id,
                Date = new DateTimeOffset(x.Date).ToUnixTimeMilliseconds()
            }).ToList();
        var tomorrowMeetings = meetings.Where(x => endDate < x.Date && x.Date < endDate.AddDays(1)).Select(x =>
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