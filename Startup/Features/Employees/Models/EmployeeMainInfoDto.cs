using Startup.Features.Meeting.Models;

namespace Startup.Features.Employees.Models;

public class EmployeeMainInfoDto
{
    public int ProgressPercentage { get; set; }
    public int UcoinsCount { get; set; }
    public int AllAchievementsCount { get; set; }
    public int CurrentAchievementsCount { get; set; }
    public int Todo { get; set; }
    public List<GetMeetingDto> TodayMeetings { get; set; }
    public List<GetMeetingDto> TommorowMeetings { get; set; }

    public class ItemTaskMainPage
    {
        public string Title { get; set; }
        public long Id { get; set; }
        public bool Checked { get; set; }
    }
}