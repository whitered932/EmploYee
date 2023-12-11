using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;

namespace Startup.Features.Meeting;

public class CreateMeetingCommand : Command
{
    
}

public sealed class CreateMeetingCommandHandler : CommandHandler<CreateMeetingCommand>
{
    public override async Task<Result> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
    {
        return Successful();
    }
}