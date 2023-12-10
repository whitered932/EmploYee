using EmploYee.Core.Models;
using EmploYee.Core.Repositories;
using Ftsoft.Storage.EntityFramework;

namespace EmploYee.Infrastructure.Storage.Repositories;

public class EmployeeRepository : EFRepository<Employee, EmploYeeDbContext>, IEmployeeRepository
{
    public EmployeeRepository(EmploYeeDbContext context) : base(context)
    {
    }
}