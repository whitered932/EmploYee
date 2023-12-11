namespace EmploYee.Core.Models;

public class Meeting : BaseEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    
    public List<Participant> Participants { get; private set; }
    public DateTime Date { get; private set; }
}

public class Participant
{
    public long UserId { get; set; }
    public string Name { get; set; }
}
