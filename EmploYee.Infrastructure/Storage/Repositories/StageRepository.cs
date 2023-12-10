using EmploYee.Core.Models;
using EmploYee.Core.Repositories;
using Ftsoft.Storage.EntityFramework;

namespace EmploYee.Infrastructure.Storage.Repositories;

public class StageRepository : EFRepository<Stage, EmploYeeDbContext>, IStageRepository

{
    public StageRepository(EmploYeeDbContext context) : base(context)
    {
    }
}