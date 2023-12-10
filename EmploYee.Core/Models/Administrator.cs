namespace EmploYee.Core.Models;

public class Administrator : User
{
    private Administrator() : base()
    {}
    
    public Administrator(string firstName, string surname, string patronymic, string email, string password,
        UserRole role, string city, string country, string name) : base(firstName, surname, patronymic, email, password,
        role, city, country, name)
    {
    }
}