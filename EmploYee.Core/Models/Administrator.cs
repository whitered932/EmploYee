namespace EmploYee.Core.Models;

public class Administrator : User
{
    private Administrator()
    {
    }

    public Administrator(
        string firstName,
        string surname,
        string patronymic,
        string email,
        string password,
        UserRole role,
        string city,
        string country,
        string name,
        DateTime birthdayUtc,
        string phoneNumber
    ) : base(firstName, surname,
        patronymic, email, password,
        role, city, country, name, birthdayUtc, phoneNumber)
    {
    }
}