namespace Startup.Features.Task.Models;

public class ItemTaskDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public bool Checked { get; set; }
}