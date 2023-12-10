using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;

namespace Startup.Features.Employees;

public class UpdateEmployeeCommand : Command
{
    
}


public sealed class UpdateEmployeeCommandHandler : CommandHandler<UpdateEmployeeCommand>
{
    public override Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}