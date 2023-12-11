using Ftsoft.Common.Result;

namespace Startup.Features.Errors;

public class NotFoundError : Error
{
    public static NotFoundError Instance => new();

    public override string Type => nameof(NotFoundError);
    
}