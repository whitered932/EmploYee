using Startup.Handlers;

namespace Startup.Providers;

public class SecurityProvider : ISecurityProvider
{
    private const string Key = "RequestSecurityProfile";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SecurityProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpContext? HttpContext => _httpContextAccessor.HttpContext;

    private SecurityProfile? GetProfile()
    {
        if (HttpContext == null)
        {
            return null;
        }

        HttpContext.Items.TryGetValue(Key, out var user);
        return user as SecurityProfile;
    }

    private void SetProfile(SecurityProfile? securityProfile)
    {
        if (HttpContext == null) throw new NullReferenceException(nameof(HttpContext));

        if (!HttpContext.Items.ContainsKey(Key))
        {
            HttpContext.Items[Key] = securityProfile;
        }
    }

    public SecurityProfile? SecurityProfile
    {
        get => GetProfile();
        set => SetProfile(value);
    }
}