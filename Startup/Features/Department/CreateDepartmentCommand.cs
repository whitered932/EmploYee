using EmploYee.Core.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;

namespace Startup.Features.Department;

public class CreateDepartmentCommand : Command
{
    public string Title { get; set; }
}

public sealed class CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository) : CommandHandler<CreateDepartmentCommand>
{
    public override async Task<Result> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = new EmploYee.Core.Models.Department(request.Title);
        await departmentRepository.AddAsync(department, cancellationToken);
        await departmentRepository.SingleAsync(cancellationToken);
        return Successful();
    }
}