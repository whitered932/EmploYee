using EmploYee.Core.Models;
using EmploYee.Core.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Employees.Models;

namespace Startup.Features.Employees;

public class CreateEmployeeCommand : Command
{
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public string City { get; set; }
    public long DepartmentId { get; set; }
    public string Curator { get; set; }
    public string Phone { get; set; }
    public string Position { get; set; }
    public long Bithdate { get; set; }
    public string Email { get; set; }
}

public sealed class CreateEmployeeCommandHandler
    (IEmployeeRepository employeeRepository) : CommandHandler<CreateEmployeeCommand>
{
    public override async Task<Result> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var date = DateTimeOffset.FromUnixTimeMilliseconds(request.Bithdate).UtcDateTime;
        var employee = new Employee(
            request.FirstName,
            request.Surname,
            request.Patronymic,
            request.Email,
            "",
            request.City,
            "Россия",
            request.City,
            request.Curator,
            request.Position,
            request.DepartmentId,
            request.Phone,
            DateTimeOffset.FromUnixTimeMilliseconds(request.Bithdate).UtcDateTime);
        await employeeRepository.AddAsync(employee, cancellationToken);
        await employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}