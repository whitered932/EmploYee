using EmploYee.Core.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Employees.Models;

namespace Startup.Features.Employees;

public class GetEmployeesQuery : Query<IReadOnlyList<EmployeeItemDto>>
{
}

public sealed class GetEmployeesQueryHandler
    (IEmployeeRepository employeeRepository) : QueryHandler<GetEmployeesQuery, IReadOnlyList<EmployeeItemDto>>
{
    public override async Task<Result<IReadOnlyList<EmployeeItemDto>>> Handle(GetEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        var employees = await employeeRepository.ListAsync(cancellationToken);
        var employeeDtos = employees.Select(employee => new EmployeeItemDto()
        {
            Id = employee.Id,
            Name = employee.GetFullName(),
            Department = "",
        }).ToList();
        return Successful(employeeDtos);
    }
}