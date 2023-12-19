using EmploYee.Core.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Achievement;
using Startup.Features.Errors;

namespace Startup.Features.Department;

public class DeleteDepartmentCommand : Command
{
    public long Id { get; set; }
}

public sealed class DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository) : CommandHandler<DeleteDepartmentCommand>
{
    public override async Task<Result> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await departmentRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (department is null)
        {
            return Error(BadRequestError.Instance);
        }
        await departmentRepository.RemoveAsync(department);
        await departmentRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}

