using EmploYee.Core.Models;
using EmploYee.Core.Repositories;
using Ftsoft.Storage.EntityFramework;

namespace EmploYee.Infrastructure.Storage.Repositories;

public class UserRepository : EFRepository<User, EmploYeeDbContext>, IUserRepository
{
    public UserRepository(EmploYeeDbContext context) : base(context)
    {
    }
}