namespace Startup.Features.Stage.Models;

public class StageDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public long? ParentStageId { get;  set; }
    public string Description { get; set; }
}

