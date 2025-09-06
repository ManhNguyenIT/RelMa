using Microsoft.EntityFrameworkCore;
using RelMa.Domain.Assets;
using RelMa.Domain.Items;
using RelMa.Domain.Locations;
using RelMa.Domain.Materials;
using RelMa.Domain.Requests;
using RelMa.Domain.Storages;
using RelMa.Domain.Users;
using RelMa.Domain.WorkOrders;
using RelMa.Infrastructure.Converters;

namespace RelMa.Infrastructure.Database;
public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options, string? tenantId = null)
    : DbContext(options)
{
    public DbSet<AssetEntity> Assets { get; set; }
    public DbSet<ItemEntity> Items { get; set; }
    public DbSet<LocationEntity> Locations { get; set; }
    public DbSet<MaterialEntity> Materials { get; set; }
    public DbSet<RequestEntity> Requests { get; set; }
    public DbSet<StorageEntity> Storages { get; set; }
    public DbSet<WorkOrderEntity> WorkOrders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(Schemas.Default);
        modelBuilder.Entity<UserEntity>().HasQueryFilter(e => e.TenantId == tenantId);
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
