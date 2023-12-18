using Ftsoft.Common.Result;

namespace Startup.Features.Errors;

public class BadRequestError : Error
{
    public override string Type => nameof(BadRequestError);
    public static BadRequestError Instance => new();
}