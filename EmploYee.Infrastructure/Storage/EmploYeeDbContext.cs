using EmploYee.Core.Models;
using Ftsoft.Storage;
using Microsoft.EntityFrameworkCore;
using Task = EmploYee.Core.Models.Task;

namespace EmploYee.Infrastructure.Storage;

public class EmploYeeDbContext : DbContext, IUnitOfWork
{
    public EmploYeeDbContext(DbContextOptions<EmploYeeDbContext> options): base(options)
    {
        
    }
    
    public EmploYeeDbContext()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=employee;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmploYeeDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<Stage> Stages { get; set; }
    public DbSet<Task> Tasks { get; set; }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<Curator> Curators { get; set; }
    public DbSet<Employee> Employees { get; set; }
}