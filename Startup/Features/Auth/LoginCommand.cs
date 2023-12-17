using System.Security.Claims;
using EmploYee.Core.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Authentication;
using Startup.Handlers;
using Startup.Helpers;
using Startup.Services;

namespace Startup.Features.Auth;

public class LoginCommand : Command
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public sealed class LoginCommandHandler(IHttpContextAccessor contextAccessor, ISessionService sessionService,
    IUserRepository userRepository) : CommandHandler<LoginCommand>
{
    public override async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (contextAccessor.HttpContext == null)
            return Error(BackgroundAuthNotSupportedError.Instance);

        var user = await userRepository.SingleOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (user == null)
            return Error(InvalidCredentialsError.Instance);

        if (!PasswordHasher.Validate(user.Password, request.Password))
            return Error(InvalidCredentialsError.Instance);

        var session = await sessionService.Create(user.Id, cancellationToken);
        var scheme = AuthHandler.Scheme;
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Sid, session.Id.ToString())
        };

        var identity = new ClaimsIdentity(claims, scheme);
        var principal = new ClaimsPrincipal(identity);
        var properties = new AuthenticationProperties
        {
            ExpiresUtc = session.ExpiresAtUtc,
            IsPersistent = true
        };
        await contextAccessor.HttpContext.SignInAsync(scheme, principal, properties);
        return Successful();
    }
}

public class InvalidCredentialsError : Error
{
    public override string Type => nameof(InvalidCredentialsError);
    public static InvalidCredentialsError Instance => new InvalidCredentialsError();
}

public class BackgroundAuthNotSupportedError : Error
{
    public override string Type => nameof(BackgroundAuthNotSupportedError);
    public static BackgroundAuthNotSupportedError Instance => new BackgroundAuthNotSupportedError();
}