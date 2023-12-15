namespace EmploYee.Core.Models;

public class Department : BaseEntity
{
    private Department() {}
    
    public Department(string title)
    {
        Title = title;
    }


    public string Title { get; private set; }
}