namespace EmploYee.Core.Models;

public class Employee : User
{
    private Employee() : base() {}
    
    public Employee(
        string firstName,
        string surname,
        string patronymic,
        string email,
        string password,
        string city,
        string country,
        string name) : base(firstName, surname, patronymic, email, password, UserRole.Employee, city, country, name)
    {
        AchievementHistories = new List<EmployeeAchievementHistory>();
    }

    public void AddAchievement(EmployeeAchievementHistory achievementHistory)
    {
        var newEmployeeAchievements = AchievementHistories.ToList();
        newEmployeeAchievements.Add(achievementHistory);
        AchievementHistories = newEmployeeAchievements;
    }
    
    public IReadOnlyCollection<EmployeeAchievementHistory> AchievementHistories { get; private set; }
}