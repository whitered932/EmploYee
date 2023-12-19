using Startup.Features.Meeting.Models;
using Startup.Features.Task.Models;

namespace Startup.Features.Employees.Models;

public class EmployeeMainInfoDto
{
    public int ProgressPercentage { get; set; }
    public double UcoinsCount { get; set; }
    public int AllAchievementsCount { get; set; }
    public int CurrentAchievementsCount { get; set; }
    public List<ItemTaskDto> Todo { get; set; }
    public List<GetMeetingDto> TodayMeetings { get; set; }
    public List<GetMeetingDto> TommorowMeetings { get; set; }
}