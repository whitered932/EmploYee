namespace Startup.Features.Meeting.Models;

public class GetMeetingDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public long Date { get; set; }
}