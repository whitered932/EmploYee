using EmploYee.Core.Models;
using EmploYee.Core.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Errors;
using Startup.Features.Task.Models;
using Startup.Providers;

namespace Startup.Features.Task;

public class GetTasksQuery : Query<IReadOnlyList<TaskDto>>
{
    public long? StageId { get; set; }
    public long? UserId { get; set; }
}

public sealed class GetTasksQueryHandler
    (ISecurityProvider securityProvider, ITaskRepository taskRepository) : QueryHandler<GetTasksQuery,
        IReadOnlyList<TaskDto>>
{
    public override async Task<Result<IReadOnlyList<TaskDto>>> Handle(GetTasksQuery request,
        CancellationToken cancellationToken)
    {
        var authorizedUser = securityProvider.SecurityProfile.User;
        var tasks = new List<EmploYee.Core.Models.Task>();
        try
        {
            tasks = (List<EmploYee.Core.Models.Task>)await taskRepository.Query(q =>
            {
                if (request.UserId is not null)
                {
                    if (authorizedUser.Role is UserRole.Employee or UserRole.Unknown)
                    {
                        throw new Exception("");
                    }

                    q = q.Where(x => x.PerformerIds.Contains((long)request.UserId));
                }

                if (request.StageId is not null)
                {
                    q = q.Where(x => x.StageId == request.StageId);
                }

                return q;
            }, cancellationToken);
        }
        catch (Exception e)
        {
            return Error(BadRequestError.Instance);
        }

        var taskDtos = tasks.Select(task => new TaskDto()
        {
            Id = task.Id,
            Description = task.Description,
            Title = task.Title,
            CurrencyValue = task.CurrencyValue,
            StageId = task.StageId,
            EndedAtUtc = task.EndedAtUtc,
            IsChecked = task.IsChecked()
        }).ToList();
        return Successful(taskDtos);
    }
}