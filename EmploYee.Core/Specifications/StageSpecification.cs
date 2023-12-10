using EmploYee.Core.Models;
using Ftsoft.Domain.Specification;

namespace EmploYee.Core.Specifications;

public class StageSpecification
{
    public static ISpecification<Stage> GetById(long id) => new Specification<Stage>(x => x.Id == id);

}