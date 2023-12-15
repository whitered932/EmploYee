using EmploYee.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmploYee.Infrastructure.Storage.TypeConfigurations;

public class MeetingTypeConfiguration : IEntityTypeConfiguration<Meeting>
{
    public void Configure(EntityTypeBuilder<Meeting> builder)
    {
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Title).IsRequired();
        builder.OwnsMany(x => x.Participants, b =>
        {
            b.Property(x => x.UserId).IsRequired();
            b.Property(x => x.Name).IsRequired();
            b.ToJson();
        });
    }
}