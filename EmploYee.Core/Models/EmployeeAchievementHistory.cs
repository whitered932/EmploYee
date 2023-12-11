namespace EmploYee.Core.Models;

public class EmployeeAchievementHistory(string title, string image, double currentValue, long? achievementId)
{
    public long? AchievementId { get; private set; } = achievementId;
    public string Title { get; private set; } = title;
    public string Image { get; private set; } = image;
    public double CurrentValue { get; private set; } = currentValue;
}