using EmploYee.Core.Repositories;
using EmploYee.Core.Specifications;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;

namespace Startup.Features.Task;

public class UpdateTaskCommand : Command
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? EndedAtUtc { get; set; }
    public long StageId { get; set; }
    public double CurrencyValue { get; set; }
}

public sealed class UpdateTaskCommandHandler(ITaskRepository taskRepository) : CommandHandler<UpdateTaskCommand>
{
    public override async Task<Result> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task =
            await taskRepository.SingleOrDefaultAsync(TaskSpecification.GetById(request.Id).IsSatisfiedBy(),
                cancellationToken);
        task.Update(
            request.Title,
            request.Description,
            request.StageId,
            request.CurrencyValue,
            request.EndedAtUtc);
        await taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Successful();
    }
}