﻿using EmploYee.Core.Models;
using EmploYee.Core.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Employees.Models;
using Startup.Helpers;

namespace Startup.Features.Employees;

public class CreateEmployeeCommand : Command
{
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public string City { get; set; }
    public long Department { get; set; }
    public string Curator { get; set; }
    public string Phone { get; set; }
    public string Position { get; set; }
    public long Bithdate { get; set; }
    public string Email { get; set; }
}

public sealed class CreateEmployeeCommandHandler
(IEmployeeRepository employeeRepository,
    IDepartmentRepository departmentRepository) : CommandHandler<CreateEmployeeCommand>
{
    public override async Task<Result> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var date = DateTimeOffset.FromUnixTimeMilliseconds(request.Bithdate).UtcDateTime;
        var department =
            await departmentRepository.SingleOrDefaultAsync(x => x.Id == request.Department, cancellationToken);

        var employee = new Employee(
            request.FirstName,
            request.Surname,
            request.Patronymic,
            request.Email,
            PasswordHasher.Hash("password"),
            request.City,
            "Россия",
            request.City,
            request.Curator,
            request.Position,
            request.Department,
            request.Phone,
            date,
            department?.Title ?? "Неизвестен");
        await employeeRepository.AddAsync(employee, cancellationToken);
        await employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}