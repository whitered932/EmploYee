namespace EmploYee.Core.Models;

public class UserAddress
{
    public UserAddress(string city, string country, string name)
    {
        City = city;
        Country = country;
        Name = name;
    }

    public string City { get; private set; }
    public string Country { get; private set; }
    public string Name { get; private set; }
}