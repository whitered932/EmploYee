using EmploYee.Core.Repositories;
using EmploYee.Core.Specifications;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Employees.Models;
using Startup.Features.Errors;
using TaskStatus = EmploYee.Core.Models.TaskStatus;

namespace Startup.Features.Employees;

public class GetEmployeeQuery : Query<GetEmployeeDto>
{
    public long Id { get; set; }
}

public sealed class GetEmployeeQueryHandler
(
    IEmployeeRepository employeeRepository,
    ITaskRepository taskRepository,
    IAchievementRepository achievementRepository,
    IDepartmentRepository departmentRepository
) : QueryHandler<GetEmployeeQuery,
    GetEmployeeDto>
{
    public override async Task<Result<GetEmployeeDto>> Handle(GetEmployeeQuery request,
        CancellationToken cancellationToken)
    {
        var employee =
            await employeeRepository.SingleOrDefaultAsync(EmployeeSpecification.GetById(request.Id).IsSatisfiedBy(),
                cancellationToken);
        if (employee is null)
        {
            return Error(NotFoundError.Instance);
        }

        var employeeTasks = await taskRepository.ListAsync(
            TaskSpecification.GetByAssignedId(employee.Id).IsSatisfiedBy(),
            cancellationToken);
        var completedCount = employeeTasks.Count(x => x.Status == TaskStatus.Approved);
        var taskCount = employeeTasks.Count;

        var achievementCount = await achievementRepository.CountAsync(cancellationToken);
        var collectedAchievementsCount = employee.AchievementHistories.Count;

        var department =
            await departmentRepository.SingleOrDefaultAsync(x => x.Id == employee.DepartmentId, cancellationToken);

        var employeeDto = new EmployeeDto()
        {
            Id = employee.Id,
            Email = employee.Email,
            Patronymic = employee.Patronymic,
            Surname = employee.Surname,
            FirstName = employee.FirstName,
            City = employee.Address.City,
            Curator = employee.Curator,
            Bithdate = new DateTimeOffset(employee.BirthdayUtc).ToUnixTimeMilliseconds(),
            Department = department?.Title ?? "Неизвестен",
            Phone = employee.PhoneNumber,
            Position = employee.Position
        };
        var getEmployeeDto = new GetEmployeeDto()
        {
            Tasks = new CountItemDto { DoneCount = completedCount, AllCount = taskCount },
            Achivements = new CountItemDto { DoneCount = collectedAchievementsCount, AllCount = achievementCount },
            Profile = employeeDto,
        };
        return Successful(getEmployeeDto);
    }
}