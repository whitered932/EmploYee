using EmploYee.Core.Models;
using EmploYee.Core.Repositories;
using Ftsoft.Storage.EntityFramework;

namespace EmploYee.Infrastructure.Storage.Repositories;

public class AdministratorRepository : EFRepository<Administrator, EmploYeeDbContext>, IAdministratorRepository
{
    public AdministratorRepository(EmploYeeDbContext context) : base(context)
    {
    }
}