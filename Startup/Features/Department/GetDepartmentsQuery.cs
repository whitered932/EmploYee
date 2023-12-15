using EmploYee.Core.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Department.Models;

namespace Startup.Features.Department;

public class GetDepartmentsQuery : Query<IReadOnlyList<DepartmentDto>>
{
    
}

public sealed class GetDepartmentsQueryHandler(IDepartmentRepository departmentRepository) : QueryHandler<GetDepartmentsQuery, IReadOnlyList<DepartmentDto>>
{
    public override async Task<Result<IReadOnlyList<DepartmentDto>>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var departments = await departmentRepository.ListAsync(cancellationToken);
        var departmentDtos = departments.Select(x => new DepartmentDto()
        {
            Title = x.Title,
            Id = x.Id
        }).ToList();
        return Successful(departmentDtos);
    }
}