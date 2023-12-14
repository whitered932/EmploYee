namespace Startup.Features.Employees.Models;

public class GetEmployeeDto
{
    public EmployeeDto Profile { get; set; }
    public CountItemDto Tasks { get; set; }
    public CountItemDto Achivements { get; set; }
}

public class EmployeeDto : ShortEmployeeDto
{
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public string City { get; set; }
    public string Department { get; set; }
    public string Curator { get; set; }
    public string Phone { get; set; }
    public string Position { get; set; }
    public long Bithdate { get; set; }
    public string Email { get; set; }
}

public class CountItemDto
{
    public int DoneCount { get; set; }
    public int AllCount { get; set; }
}
