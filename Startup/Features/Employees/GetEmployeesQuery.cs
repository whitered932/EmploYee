using EmploYee.Core.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Employees.Models;

namespace Startup.Features.Employees;

public class GetEmployeesQuery : Query<IReadOnlyList<EmployeeDto>>
{
}

public sealed class GetEmployeesQueryHandler
    (IEmployeeRepository employeeRepository) : QueryHandler<GetEmployeesQuery, IReadOnlyList<EmployeeDto>>
{
    public override async Task<Result<IReadOnlyList<EmployeeDto>>> Handle(GetEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        var employees = await employeeRepository.ListAsync(cancellationToken);
        var employeeDtos = employees.Select(employee => new EmployeeDto()
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
            }
        }).ToList();
        return Successful(employeeDtos);
    }
}