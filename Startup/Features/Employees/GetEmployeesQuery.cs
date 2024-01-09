using EmploYee.Core.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Employees.Models;

namespace Startup.Features.Employees;

public class GetEmployeesQuery : Query<IReadOnlyList<EmployeeItemDto>>
{
    public long? DepartmentId { get; set; }
}

public sealed class GetEmployeesQueryHandler
    (IEmployeeRepository employeeRepository) : QueryHandler<GetEmployeesQuery, IReadOnlyList<EmployeeItemDto>>
{
    public override async Task<Result<IReadOnlyList<EmployeeItemDto>>> Handle(GetEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        var employees = await employeeRepository.Query(q =>
        {
            if (request.DepartmentId is not null)
            {
                q = q.Where(x => x.DepartmentId == request.DepartmentId);
            }

            return q;
        }, cancellationToken);
        var employeeDtos = employees.Select(employee => new EmployeeItemDto()
        {
            Id = employee.Id,
            Name = employee.GetFullName(),
            Department = employee.DepartmentTitle,
        }).ToList();
        return Successful(employeeDtos);
    }
}