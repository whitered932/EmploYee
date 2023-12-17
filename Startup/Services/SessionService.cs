using EmploYee.Core.Models;
using EmploYee.Core.Repositories;
using Task = System.Threading.Tasks.Task;

namespace Startup.Services;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _sessionRepository;

    private static TimeSpan ExpirationSpan => TimeSpan.FromDays(1);

    public SessionService(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    /// <summary>
    /// Создание сессии
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="installationId">id устройства</param>
    /// <returns></returns>
    public async Task<Session> Create(long userId, CancellationToken cancellationToken)
    {
        var expirationTime = DateTime.UtcNow + ExpirationSpan;
        var session = new Session(expirationTime, userId);
        await _sessionRepository.AddAsync(session, cancellationToken);
        await _sessionRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return session;
    }

    public async Task<Session> GetAsync(long sid, CancellationToken cancellationToken)
    {
        var session = await _sessionRepository.SingleOrDefaultAsync(x => x.Id == sid, cancellationToken);
        return session;
    }

    public async Task RemoveAsync(long sid, CancellationToken cancellationToken)
    {
        var session = await _sessionRepository.SingleOrDefaultAsync(x => x.Id == sid, cancellationToken);
        if (session == null)
            return;

        await _sessionRepository.RemoveAsync(session);
        await _sessionRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}