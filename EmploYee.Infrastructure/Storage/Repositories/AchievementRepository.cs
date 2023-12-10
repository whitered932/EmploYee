using EmploYee.Core.Models;
using EmploYee.Core.Repositories;
using Ftsoft.Storage.EntityFramework;

namespace EmploYee.Infrastructure.Storage.Repositories;

public class AchievementRepository : EFRepository<Achievement, EmploYeeDbContext>, IAchievementRepository
{
    public AchievementRepository(EmploYeeDbContext context) : base(context)
    {
    }
}