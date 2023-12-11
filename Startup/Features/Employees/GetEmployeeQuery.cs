using EmploYee.Core.Repositories;
using EmploYee.Core.Specifications;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Employees.Models;
using Startup.Features.Errors;
using TaskStatus = EmploYee.Core.Models.TaskStatus;

namespace Startup.Features.Employees;

public class GetEmployeeQuery : Query<EmployeeDto>
{
    public long Id { get; set; }
}

public sealed class GetEmployeeQueryHandler
(
    IEmployeeRepository employeeRepository,
    ITaskRepository taskRepository,
    IAchievementRepository achievementRepository
) : QueryHandler<GetEmployeeQuery,
    EmployeeDto>
{
    public override async Task<Result<EmployeeDto>> Handle(GetEmployeeQuery request,
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
            TaskSpecification.GetByPerformerId(employee.Id).IsSatisfiedBy(),
            cancellationToken);
        var completedCount = employeeTasks.Count(x => x.Status == TaskStatus.Approved);
        var taskCount = employeeTasks.Count;

        var achievementCount = await achievementRepository.CountAsync(cancellationToken);
        var collectedAchievementsCount = employee.AchievementHistories.Count;
        
        var employeeDto = new EmployeeDto()
        {
            Id = employee.Id,
            Email = employee.Email,
            Patronymic = employee.Patronymic,
            Surname = employee.Surname,
            FirstName = employee.FirstName,
            Address = new UserAddressDto()
            {
                City = employee.Address.City,
                Country = employee.Address.Country,
                Name = employee.Address.Name,
            },
            TaskCount = taskCount,
            CompletedTaskCount = completedCount
        };
        return Successful(employeeDto);
    }
}