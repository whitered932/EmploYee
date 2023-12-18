using EmploYee.Core.Specifications;

namespace EmploYee.Core.Models;

public class Task(string title, string description, long stageId, double currencyValue, DateTime? endedAtUtc, List<long> performerIds)
    : BaseEntity
{
    public void Update(string title, string description, long stageId, double currencyValue, DateTime? endedAtUtc)
    {
        Title = title;
        Description = description;
        EndedAtUtc = endedAtUtc;
        StageId = stageId;
        CurrencyValue = currencyValue;
        Status = TaskStatus.Opened;
    }

    public TaskStatus Status { get; private set; }
    public List<long> PerformerIds { get; private set; } = performerIds;
    public string Title { get; private set; } = title;
    public string Description { get; private set; } = description;
    public DateTime? EndedAtUtc { get; private set; } = endedAtUtc;
    public long StageId { get; private set; } = stageId;
    public double CurrencyValue { get; private set; } = currencyValue;

    public void UpdateStatus(TaskStatus status)
    {
        Status = status;
    }

    public bool IsChecked()
    {
        return Status is TaskStatus.Ready;
    }
}