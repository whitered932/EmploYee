namespace EmploYee.Core.Models;

public class User : BaseEntity
{
    protected User()
    {
    }
    
    public User(
        string firstName, 
        string surname,
        string patronymic,
        string email, 
        string password, 
        UserRole role,
        string city,
        string country,
        string name)
    {
        FirstName = firstName;
        Surname = surname;
        Patronymic = patronymic;
        Email = email;
        Password = password;
        Role = role;
        Address = new UserAddress(city, country, name);
    }

    public string FirstName { get; private set; }
    public string Surname { get; private set; }
    public string Patronymic { get; private set; }
    public DateTime BirthdayUtc { get; private set; }

    public string Email { get; private set; }
    public string Password { get; private set; }

    public UserRole Role { get; private set; }
    public UserAddress Address { get; private set; }
}