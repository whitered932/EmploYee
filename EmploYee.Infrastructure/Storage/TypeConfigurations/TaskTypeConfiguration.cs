using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = EmploYee.Core.Models.Task;
namespace EmploYee.Infrastructure.Storage.TypeConfigurations;

public class TaskTypeConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
    }
}