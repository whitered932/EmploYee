﻿namespace EmploYee.Core.Models;

public class Employee : User
{
    private Employee() {}
    
    public Employee(
        string firstName,
        string surname,
        string patronymic,
        string email,
        string password,
        string city,
        string country,
        string name,
        string curator,
        string position,
        long departmentId,
        string phoneNumber,
        DateTime birthDateTime
    ) : base(firstName, surname, patronymic, email, password, UserRole.Employee, city, country, name, birthDateTime)
    {
        Curator = curator;
        Position = position;
        DepartmentId = departmentId;
        PhoneNumber = phoneNumber;
        AchievementHistories = new List<EmployeeAchievementHistory>();
    }

    public IReadOnlyCollection<EmployeeAchievementHistory> AchievementHistories { get; private set; }
    public string PhoneNumber { get; private set; }
    public long DepartmentId { get; private set; }
    public string Position { get; private set; }
    public string Curator { get; private set; }

    public void AddAchievement(EmployeeAchievementHistory achievementHistory)
    {
        var newEmployeeAchievements = AchievementHistories.ToList();
        newEmployeeAchievements.Add(achievementHistory);
        AchievementHistories = newEmployeeAchievements;
    }
}