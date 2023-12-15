using EmploYee.Core.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Meeting.Models;

namespace Startup.Features.Meeting;

public sealed class GetMeetingsQuery : Query<IReadOnlyList<GetMeetingDto>>
{
    
}

public sealed class GetMeetingsQueryHandler(IMeetingRepository meetingRepository) : QueryHandler<GetMeetingsQuery, IReadOnlyList<GetMeetingDto>>
{
    public override async Task<Result<IReadOnlyList<GetMeetingDto>>> Handle(GetMeetingsQuery request, CancellationToken cancellationToken)
    {
        var meetings = await meetingRepository.ListAsync(cancellationToken);
        var meetingsDto = meetings.Select(x => new GetMeetingDto()
        {
            Date = new DateTimeOffset(x.Date).ToUnixTimeMilliseconds(),
            Description = x.Description,
            Id = x.Id,
            Title = x.Title
        }).ToList();
        return Successful(meetingsDto);
    }
}