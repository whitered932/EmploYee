namespace EmploYee.Core.Models;

public class Meeting : BaseEntity
{
    private Meeting()
    {
    }

    public Meeting(string title, string description, IEnumerable<User> participants, DateTime date)
    {
        Title = title;
        Description = description;
        Date = date;

        Participants = participants.Select(x => new MeetingParticipant()
        {
            Name = x.GetFullName(),
            UserId = x.Id,
        }).ToList();
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public List<MeetingParticipant> Participants { get; private set; }
    public DateTime Date { get; private set; }

    public class MeetingParticipant
    {
        public long UserId { get; set; }
        public string Name { get; set; }
    }
}