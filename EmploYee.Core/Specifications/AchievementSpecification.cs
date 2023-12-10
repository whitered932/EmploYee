using EmploYee.Core.Models;
using Ftsoft.Domain.Specification;

namespace EmploYee.Core.Specifications;

public class AchievementSpecification
{
    public static ISpecification<Achievement> GetById(long id) => new Specification<Achievement>(x => x.Id == id);

}