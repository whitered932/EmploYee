namespace Startup.Features.Achievement.Models;

public class AchievementDto
{
    public long Id { get; set; }
    public string Title { get; set; } 
    public string Description { get; set; } 
    public string Img { get; set; } 
    public bool IsCompleted { get; set; } 
    public double CurrentValue { get; set; } 

}