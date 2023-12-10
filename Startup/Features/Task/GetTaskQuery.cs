using EmploYee.Core.Repositories;
using EmploYee.Core.Specifications;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Task.Models;

namespace Startup.Features.Task;

public class GetTaskQuery : Query<TaskDto>
{
    public long Id { get; set; }
}

public sealed class GetTaskQueryHandler
    (ITaskRepository taskRepository) : QueryHandler<GetTaskQuery, TaskDto>
{
    public override async Task<Result<TaskDto>> Handle(GetTaskQuery request,
        CancellationToken cancellationToken)
    {
        var task =
            await taskRepository.SingleOrDefaultAsync(TaskSpecification.GetById(request.Id).IsSatisfiedBy(),
                cancellationToken);
        var taskDto = new TaskDto()
        {
            Id = task.Id,
            Description = task.Description,
            Title = task.Title,
            CurrencyValue = task.CurrencyValue,
            StageId = task.StageId,
            EndedAtUtc = task.EndedAtUtc
        };
        return Successful(taskDto);
    }
}