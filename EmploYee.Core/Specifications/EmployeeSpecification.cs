using EmploYee.Core.Models;
using Ftsoft.Domain.Specification;

namespace EmploYee.Core.Specifications;

public static class EmployeeSpecification
{
    public static ISpecification<Employee> GetById(long id) => new Specification<Employee>(x => x.Id == id);
}