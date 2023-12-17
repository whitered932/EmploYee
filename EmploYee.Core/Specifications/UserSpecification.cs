using EmploYee.Core.Models;
using Ftsoft.Domain.Specification;

namespace EmploYee.Core.Specifications;

public class UserSpecification
{
    public static ISpecification<User> GetByIds(List<long> ids) => new Specification<User>(x => ids.Contains(x.Id));
    public static ISpecification<User> GetById(long id) => new Specification<User>(x => x.Id == id);

}