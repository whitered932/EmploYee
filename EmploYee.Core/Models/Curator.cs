namespace EmploYee.Core.Models;

public class Curator : User
{
    private Curator() : base() {}
    
    public Curator(string firstName, string surname, string patronymic, string email, string password, UserRole role,
        string city, string country, string name) : base(firstName, surname, patronymic, email, password, role, city,
        country, name)
    {
    }
}