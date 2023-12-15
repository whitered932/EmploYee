using EmploYee.Core.Models;
using Ftsoft.Domain.Specification;

namespace EmploYee.Core.Specifications;

public class MeetingSpecification
{
    public static ISpecification<Meeting> GetByParticipantId(long id, DateTime startUtc, DateTime endUtc) =>
        new Specification<Meeting>(x => x.Participants.Select(p => p.UserId).Contains(id) && startUtc < x.Date && x.Date < endUtc);
}