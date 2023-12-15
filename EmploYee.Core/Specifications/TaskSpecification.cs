using Ftsoft.Domain.Specification;
using Task = EmploYee.Core.Models.Task;

namespace EmploYee.Core.Specifications;

public static class TaskSpecification
{
    public static ISpecification<Task> GetById(long id) =>
        new Specification<Task>(x => x.Id == id);
    
    public static ISpecification<Task> GetByAssignedId(long performerId) =>
        new Specification<Task>(x => x.PerformerIds.Contains(performerId));
}