using EmploYee.Core.Models;
using EmploYee.Core.Repositories;
using Ftsoft.Storage.EntityFramework;

namespace EmploYee.Infrastructure.Storage.Repositories;

public class SessionRepository : EFRepository<Session, EmploYeeDbContext>, ISessionRepository
{
    public SessionRepository(EmploYeeDbContext context) : base(context)
    {
    }
}