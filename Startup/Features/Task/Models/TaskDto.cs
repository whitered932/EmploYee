namespace Startup.Features.Task.Models;

public class TaskDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? EndedAtUtc { get; set; }
    public long StageId { get; set; }
    public double CurrencyValue { get; set; }
}