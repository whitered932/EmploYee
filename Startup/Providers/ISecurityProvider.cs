using Startup.Handlers;

namespace Startup.Providers;

public interface ISecurityProvider
{
    public SecurityProfile SecurityProfile { get; set; }
}