using EmploYee.Core.Repositories;
using Ftsoft.Storage.EntityFramework;
using Task = EmploYee.Core.Models.Task;

namespace EmploYee.Infrastructure.Storage.Repositories;

public class TaskRepository : EFRepository<Task, EmploYeeDbContext>, ITaskRepository
{
    public TaskRepository(EmploYeeDbContext context) : base(context)
    {
    }
}