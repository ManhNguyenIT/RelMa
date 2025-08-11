using Microsoft.EntityFrameworkCore;
using RelMa.Domain.Todos;
using RelMa.Infrastructure.Converters;

namespace RelMa.Infrastructure.Database;
public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<TodoEntity> Todos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Default);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<Ulid>()
            .HaveConversion<UlidToStringConverter>();

        base.ConfigureConventions(configurationBuilder);
    }
}
