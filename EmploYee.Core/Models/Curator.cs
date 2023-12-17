namespace EmploYee.Core.Models;

public class Curator : User
{
    private Curator() {}
    public Curator(string firstName, string surname, string patronymic, string email, string password, UserRole role,
        string city, string country, string name, DateTime birthDateUtc, string phoneNumber) : base(firstName, surname, patronymic, email, password, role, city,
        country, name, birthDateUtc, phoneNumber)
    {
    }
}