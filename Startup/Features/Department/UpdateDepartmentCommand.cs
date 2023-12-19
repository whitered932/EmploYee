using EmploYee.Core.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Errors;

namespace Startup.Features.Department;

public class UpdateDepartmentCommand : Command
{
    public long Id { get; set; }
    public string Title { get; set; }
}

public sealed class UpdateDepartmentCommandHandler
    (IDepartmentRepository departmentRepository) : CommandHandler<UpdateDepartmentCommand>
{
    public override async Task<Result> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await departmentRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (department is null)
        {
            return Error(BadRequestError.Instance);
        }

        department.Update(request.Title);
        await departmentRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}