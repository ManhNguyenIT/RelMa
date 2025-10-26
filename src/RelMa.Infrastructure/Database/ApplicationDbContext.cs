using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Domain.AssetLogs;
using RelMa.Domain.Assets;
using RelMa.Domain.Checklists;
using RelMa.Domain.Files;
using RelMa.Domain.Locations;
using RelMa.Domain.Maintenances;
using RelMa.Domain.Materials;
using RelMa.Domain.OutboxMessages;
using RelMa.Domain.Parts;
using RelMa.Domain.Requests;
using RelMa.Domain.Sequences;
using RelMa.Domain.Sets;
using RelMa.Domain.Storages;
using RelMa.Domain.Tasks;
using RelMa.Domain.Teams;
using RelMa.Domain.Users;
using RelMa.Domain.WorkOrders;

namespace RelMa.Infrastructure.Database;
public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IUserContext? userContext = null)
    : DbContext(options)
{
    public DbSet<AssetEntity> Assets { get; set; }
    public DbSet<AssetLogEntity> AssetLogs { get; set; }
    public DbSet<ChecklistEntity> Checklists { get; set; }
    public DbSet<FileEntity> Files { get; set; }
    public DbSet<LocationEntity> Locations { get; set; }
    public DbSet<MaintenanceEntity> Maintenances { get; set; }
    public DbSet<MaterialEntity> Materials { get; set; }
    public DbSet<PartEntity> Parts { get; set; }
    public DbSet<RequestEntity> Requests { get; set; }
    public DbSet<SetEntity> Sets { get; set; }
    public DbSet<StorageEntity> Storages { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<TeamEntity> Teams { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<WorkOrderEntity> WorkOrders { get; set; }
    public DbSet<SequenceEntity> Sequences { get; set; }
    public DbSet<OutboxMessageEntity> OutboxMessages { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(Schemas.Default);
        if (userContext is not null)
        {
            modelBuilder.Entity<AssetEntity>().HasQueryFilter(e => e.TenantId == userContext.TenantId);
            modelBuilder.Entity<AssetLogEntity>().HasQueryFilter(e => e.TenantId == userContext.TenantId);
            modelBuilder.Entity<ChecklistEntity>().HasQueryFilter(e => e.TenantId == userContext.TenantId);
            modelBuilder.Entity<FileEntity>().HasQueryFilter(e => e.TenantId == userContext.TenantId);
            modelBuilder.Entity<LocationEntity>().HasQueryFilter(e => e.TenantId == userContext.TenantId);
            modelBuilder.Entity<MaintenanceEntity>().HasQueryFilter(e => e.TenantId == userContext.TenantId);
            modelBuilder.Entity<MaterialEntity>().HasQueryFilter(e => e.TenantId == userContext.TenantId);
            modelBuilder.Entity<PartEntity>().HasQueryFilter(e => e.TenantId == userContext.TenantId);
            modelBuilder.Entity<RequestEntity>().HasQueryFilter(e => e.TenantId == userContext.TenantId);
            modelBuilder.Entity<SetEntity>().HasQueryFilter(e => e.TenantId == userContext.TenantId);
            modelBuilder.Entity<StorageEntity>().HasQueryFilter(e => e.TenantId == userContext.TenantId);
            modelBuilder.Entity<TaskEntity>().HasQueryFilter(e => e.TenantId == userContext.TenantId);
            modelBuilder.Entity<TeamEntity>().HasQueryFilter(e => e.TenantId == userContext.TenantId);
            modelBuilder.Entity<UserEntity>().HasQueryFilter(e => e.TenantId == userContext.TenantId);
            modelBuilder.Entity<WorkOrderEntity>().HasQueryFilter(e => e.TenantId == userContext.TenantId);
            modelBuilder.Entity<SequenceEntity>().HasQueryFilter(e => e.TenantId == userContext.TenantId);
            modelBuilder.Entity<OutboxMessageEntity>().HasQueryFilter(e => e.TenantId == userContext.TenantId);
        }
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
