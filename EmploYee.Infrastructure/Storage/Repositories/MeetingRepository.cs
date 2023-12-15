using EmploYee.Core.Models;
using EmploYee.Core.Repositories;
using Ftsoft.Storage.EntityFramework;

namespace EmploYee.Infrastructure.Storage.Repositories;

public class MeetingRepository : EFRepository<Meeting, EmploYeeDbContext>, IMeetingRepository
{
    public MeetingRepository(EmploYeeDbContext context) : base(context)
    {
    }
}