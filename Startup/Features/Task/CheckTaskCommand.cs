using EmploYee.Core.Repositories;
using EmploYee.Core.Specifications;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Errors;
using Startup.Providers;
using TaskStatus = EmploYee.Core.Models.TaskStatus;

namespace Startup.Features.Task;

public class CheckTaskCommand : Command
{
    public long Id { get; set; }
}

public sealed class CheckTaskCommandHandler(ITaskRepository taskRepository, ISecurityProvider securityProvider,
    IEmployeeRepository employeeRepository) : CommandHandler<CheckTaskCommand>
{
    public override async Task<Result> Handle(CheckTaskCommand request, CancellationToken cancellationToken)
    {
        var securityUser = securityProvider.SecurityProfile.User;
        var user = await employeeRepository.SingleOrDefaultAsync(
            EmployeeSpecification.GetById(securityUser.Id).IsSatisfiedBy(), cancellationToken);
        if (user is null)
        {
            return Error(BadRequestError.Instance);
        }


        var task = await taskRepository.SingleOrDefaultAsync(TaskSpecification.GetById(request.Id).IsSatisfiedBy(),
            cancellationToken);
        if (task is null)
        {
            return Error(NotFoundError.Instance);
        }

        if (!task.PerformerIds.Contains(user.Id))
        {
            return Error(BadRequestError.Instance);
        }


        var isChecked = task.IsChecked();
        var status = isChecked ? TaskStatus.Reopened : TaskStatus.Ready;
        
        task.UpdateStatus(status);
        if (task.Status is TaskStatus.Ready)
        {
            user.Debit(task.CurrencyValue);
        }
        else
        {
            user.Credit(task.CurrencyValue);
        }

        await taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful(task);
    }
}