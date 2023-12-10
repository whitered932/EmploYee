using EmploYee.Core.Repositories;
using EmploYee.Core.Specifications;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;

namespace Startup.Features.Task;

public class DeleteTaskCommand : Command
{
    public long Id { get; set; }
}

public sealed class DeleteTaskCommandHandler(ITaskRepository taskRepository) : CommandHandler<DeleteTaskCommand>
{
    public override async Task<Result> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task =
            await taskRepository.SingleOrDefaultAsync(TaskSpecification.GetById(request.Id).IsSatisfiedBy(),
                cancellationToken);
        await taskRepository.RemoveAsync(task);
        await taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}