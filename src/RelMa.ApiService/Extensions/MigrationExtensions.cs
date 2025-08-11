using Microsoft.EntityFrameworkCore;
using RelMa.Infrastructure.Database;

namespace RelMa.ApiService.Extensions;

internal static class MigrationExtensions
{
    public static async Task ApplyMigrations(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return;
        }
        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();
    }
}
