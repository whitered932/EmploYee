namespace Startup.Features.Stage.Models;

public class StageDto
{
    public string Title { get; set; }
    public long? ParentStageId { get;  set; }
    public string Description { get; set; }
}

