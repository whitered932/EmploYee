using EmploYee.Core.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using EmployeeTask = EmploYee.Core.Models.Task;

namespace Startup.Features.Task;

public class CreateTaskCommand : Command
{
    public string Title { get; set; }
    public string Description { get; set; }
    public long EndedAtUtc { get; set; }
    public long? StageId { get; set; }
    public double CurrencyValue { get; set; }
    public List<long> AssignedIds { get; set; }
}

public sealed class CreateTaskCommandHandler
    (ITaskRepository taskRepository) : CommandHandler<CreateTaskCommand>
{
    public override async Task<Result> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new EmploYee.Core.Models.Task(
            request.Title,
            request.Description,
            request.StageId ?? 0,
            request.CurrencyValue,
            DateTimeOffset.FromUnixTimeMilliseconds(request.EndedAtUtc).UtcDateTime,
            request.AssignedIds);
        await taskRepository.AddAsync(task, cancellationToken);
        await taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}