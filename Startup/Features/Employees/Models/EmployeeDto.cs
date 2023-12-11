namespace Startup.Features.Employees.Models;

public class EmployeeDto : ShortEmployeeDto
{
    public UserAddressDto Address { get; set; }
    public int TaskCount { get; set; }
    public int CompletedTaskCount { get; set; }
    public int AchievementsCollected { get; set; }
    public int TotalAchievements { get; set; }
}

public class UserAddressDto
{
    public string City { get; set; }
    public string Country { get; set; }
    public string Name { get; set; }
}