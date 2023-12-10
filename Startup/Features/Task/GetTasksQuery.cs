using EmploYee.Core.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Task.Models;

namespace Startup.Features.Task;

public class GetTasksQuery : Query<IReadOnlyList<TaskDto>>
{
}

public sealed class GetTasksQueryHandler
    (ITaskRepository taskRepository) : QueryHandler<GetTasksQuery, IReadOnlyList<TaskDto>>
{
    public override async Task<Result<IReadOnlyList<TaskDto>>> Handle(GetTasksQuery request,
        CancellationToken cancellationToken)
    {
        var employees = await taskRepository.ListAsync(cancellationToken);
        var employeeDtos = employees.Select(task => new TaskDto()
        {
            Id = task.Id,
            Description = task.Description,
            Title = task.Title,
            CurrencyValue = task.CurrencyValue,
            StageId = task.StageId,
            EndedAtUtc = task.EndedAtUtc
        }).ToList();
        return Successful(employeeDtos);
    }
}