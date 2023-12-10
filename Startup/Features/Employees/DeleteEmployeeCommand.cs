using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;

namespace Startup.Features.Employees;

public class DeleteEmployeeCommand : Command
{
    public long Id { get; set; }
}

public sealed class DeleteEmployeeCommandHandler : CommandHandler<DeleteEmployeeCommand>
{
    public override Task<Result> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}