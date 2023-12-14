using EmploYee.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmploYee.Infrastructure.Storage.TypeConfigurations;

public class EmployeeTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.OwnsMany(x => x.AchievementHistories, (o) =>
        {
            o.ToJson();
        });
    }
}