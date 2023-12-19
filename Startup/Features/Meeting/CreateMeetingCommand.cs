using EmploYee.Core.Repositories;
using EmploYee.Core.Specifications;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;

namespace Startup.Features.Meeting;

public class CreateMeetingCommand : Command
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required long DateAtUtc { get; set; }
    public required List<long> Assigned { get; set; }
}

public sealed class CreateMeetingCommandHandler(IMeetingRepository meetingRepository, IUserRepository userRepository) : CommandHandler<CreateMeetingCommand>
{
    public override async Task<Result> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
    {
        var participants = await userRepository.ListAsync(UserSpecification.GetByIds(request.Assigned).IsSatisfiedBy(), cancellationToken);
        var date = DateTimeOffset.FromUnixTimeMilliseconds(request.DateAtUtc).UtcDateTime;
        var meeting = new EmploYee.Core.Models.Meeting(request.Title, request.Description, participants, date);
        await meetingRepository.AddAsync(meeting, cancellationToken);
        await meetingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}