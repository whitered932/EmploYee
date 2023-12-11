using EmploYee.Core.Repositories;
using EmploYee.Core.Specifications;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Errors;

namespace Startup.Features.Task;

public class UpdateTaskStatusCommand : Command
{
    public long Id { get; set; }
    public EmploYee.Core.Models.TaskStatus Status { get; set; }
}

public sealed class UpdateTaskStatusCommandHandler(ITaskRepository taskRepository) : CommandHandler<UpdateTaskStatusCommand>
{
    public override async Task<Result> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var task =
            await taskRepository.SingleOrDefaultAsync(TaskSpecification.GetById(request.Id).IsSatisfiedBy(),
                cancellationToken);
        if (task is null)
        {
            return Error(NotFoundError.Instance);
        }
        task.UpdateStatus(request.Status);
        await taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}