namespace EmploYee.Core.Models;

public class Stage : BaseEntity
{
    private Stage()
    {
    }

    public Stage(string title, long? parentStageId, string description)
    {
        Title = title;
        ParentStageId = parentStageId;
        Description = description;
    }

    public string Title { get; private set; }
    public long? ParentStageId { get; private set; }
    public string Description { get; private set; }

    public void Update(string title, string description)
    {
        Title = title;
        Description = description;
    }
}