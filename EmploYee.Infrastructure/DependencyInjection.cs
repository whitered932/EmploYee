using EmploYee.Core.Repositories;
using EmploYee.Infrastructure.Storage;
using EmploYee.Infrastructure.Storage.Repositories;
using Ftsoft.Storage.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmploYee.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string dbConnectionString)
    {
        services.AddDbContext<EmploYeeDbContext>();

        services.RegisterRepository<IEmployeeRepository, EmployeeRepository>();
        services.RegisterRepository<ITaskRepository, TaskRepository>();
        services.RegisterRepository<IUserRepository, UserRepository>();
        services.RegisterRepository<IStageRepository, StageRepository>();
        services.RegisterRepository<ICuratorRepository, CuratorRepository>();
        services.RegisterRepository<IAdministratorRepository, AdministratorRepository>();
        services.RegisterRepository<IAchievementRepository, AchievementRepository>();
        services.RegisterRepository<IMeetingRepository, MeetingRepository>();
        services.RegisterRepository<IDepartmentRepository, DepartmentRepository>();

        return services;
    }
}