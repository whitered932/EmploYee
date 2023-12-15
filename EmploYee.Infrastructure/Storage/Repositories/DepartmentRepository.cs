using EmploYee.Core.Models;
using EmploYee.Core.Repositories;
using Ftsoft.Storage.EntityFramework;

namespace EmploYee.Infrastructure.Storage.Repositories;

public class DepartmentRepository : EFRepository<Department, EmploYeeDbContext>, IDepartmentRepository
{
    public DepartmentRepository(EmploYeeDbContext context) : base(context)
    {
    }
}