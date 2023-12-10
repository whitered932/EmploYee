using EmploYee.Core.Models;
using EmploYee.Core.Repositories;
using Ftsoft.Storage.EntityFramework;

namespace EmploYee.Infrastructure.Storage.Repositories;

public class CuratorRepository : EFRepository<Curator, EmploYeeDbContext>, ICuratorRepository
{
    public CuratorRepository(EmploYeeDbContext context) : base(context)
    {
    }
}