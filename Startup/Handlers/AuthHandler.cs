using System.Security.Claims;
using EmploYee.Core.Models;
using EmploYee.Core.Repositories;
using EmploYee.Core.Specifications;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Startup.Providers;
using Task = System.Threading.Tasks.Task;

namespace Startup.Handlers;

public class AuthHandler(ISecurityProvider securityProvider,
    ISessionRepository sessionRepository,
    IUserRepository userRepository) : CookieAuthenticationEvents
{
    public static readonly string Scheme = "Cookie";

    public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
    {
        var userPrincipal = context.Principal;

        var sidValue = userPrincipal?.Claims
            .Where(x => x.Type == ClaimTypes.Sid)
            .Select(x => x.Value)
            .FirstOrDefault();

        if (string.IsNullOrEmpty(sidValue))
        {
            await SignOutAsync(context, CancellationToken.None);
        }

        long.TryParse(sidValue!, out var sid);

        var cancellationToken = context.HttpContext.RequestAborted;
        var (isSuccess, securityProfile) = await Login(sid, cancellationToken);

        if (!isSuccess)
        {
            await SignOutAsync(context, cancellationToken);
            return;
        }

        if (securityProfile is not EmptySecurityProfile)
        {
            securityProvider.SecurityProfile = securityProfile!;
        }
    }

    private async Task<(bool, SecurityProfile)> Login(long sid, CancellationToken cancellationToken)
    {
        var session = await sessionRepository.SingleOrDefaultAsync(x => x.Id == sid, cancellationToken);

        if (session is null)
        {
            return (false, EmptySecurityProfile.Instance);
        }

        var user = await userRepository.SingleOrDefaultAsync(UserSpecification.GetById(session.UserId).IsSatisfiedBy(),
            cancellationToken);
        if (user is null)
        {
            return (false, EmptySecurityProfile.Instance);
        }

        var securityUser = new SecurityUser()
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role,
            Name = user.GetFullName()
        };
        return (true, new SecurityProfile(securityUser, session));
    }

    private async Task SignOutAsync(CookieValidatePrincipalContext context, CancellationToken cancellationToken)
    {
        context.RejectPrincipal();
        await context.HttpContext.SignOutAsync(Scheme);
    }
}

public class SecurityProfile
{
    private SecurityProfile()
    {
    }

    public SecurityProfile(SecurityUser user, Session session)
    {
        User = user;
        Session = session;
    }

    public SecurityUser User { get; set; }
    public Session Session { get; set; }
}

public class EmptySecurityProfile : SecurityProfile
{
    public static SecurityProfile Instance => new SecurityProfile(null, null);

    public EmptySecurityProfile(SecurityUser securityUser, Session session) : base(securityUser, session)
    {
    }
}

public class SecurityUser
{
    public UserRole Role { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public long Id { get; set; }
}