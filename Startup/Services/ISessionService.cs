using EmploYee.Core.Models;
using Task = System.Threading.Tasks.Task;

namespace Startup.Services;

public interface ISessionService
{
    public Task<Session> Create(long userId, CancellationToken cancellationToken);
    public Task<Session> GetAsync(long sid, CancellationToken cancellationToken);
    public Task RemoveAsync(long sid, CancellationToken cancellationToken);
}