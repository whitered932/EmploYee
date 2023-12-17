using EmploYee.Core.Models;
using EmploYee.Infrastructure.Storage.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmploYee.Infrastructure.Storage.TypeConfigurations;

public class UserTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasDiscriminator<UserRole>(x => x.Role)
            .HasValue<User>(UserRole.Unknown)
            .HasValue<Administrator>(UserRole.Administrator)
            .HasValue<Employee>(UserRole.Employee)
            .HasValue<Curator>(UserRole.Curator);
        builder.OwnsOne(x => x.Address, b =>
        {
            b.Property(x => x.City);
            b.Property(x => x.Country);
            b.Property(x => x.Name);
        });
    }
}