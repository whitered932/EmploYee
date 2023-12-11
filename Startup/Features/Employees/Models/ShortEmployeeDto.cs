namespace Startup.Features.Employees.Models;

public class ShortEmployeeDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }

    public string Email { get; set; }
}