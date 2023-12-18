using EmploYee.Core.Repositories;
using EmploYee.Core.Specifications;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Errors;
using TaskStatus = EmploYee.Core.Models.TaskStatus;

namespace Startup.Features.Task;

public class CheckTaskCommand : Command
{
    public long Id { get; set; }
}

public sealed class CheckTaskCommandHandler(ITaskRepository taskRepository) : CommandHandler<CheckTaskCommand>
{
    public override async Task<Result> Handle(CheckTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.SingleOrDefaultAsync(TaskSpecification.GetById(request.Id).IsSatisfiedBy(),
            cancellationToken);
        if (task is null)
        {
            return Error(NotFoundError.Instance);
        }

        var isChecked = task.IsChecked();
        task.UpdateStatus(isChecked ? TaskStatus.Reopened : TaskStatus.Ready);
        await taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful(task);
    }
}