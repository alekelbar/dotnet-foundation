using AppTemplate.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AppTemplate.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string? connectionString,
        string contentRootPath)
    {
        var sqliteConnection = new SqliteConnectionStringBuilder(
            connectionString ?? throw new InvalidOperationException("The DefaultConnection connection string is required."));

        // Resolve relative database paths against the host's content root.
        sqliteConnection.DataSource = Path.GetFullPath(sqliteConnection.DataSource, contentRootPath);

        services.AddDbContextFactory<AppDbContext>(options =>
            options.UseSqlite(sqliteConnection.ToString()));

        return services;
    }

    public static async Task InitializeDatabaseAsync(
        this IServiceProvider services,
        CancellationToken cancellationToken = default)
    {
        var contextFactory = services.GetRequiredService<IDbContextFactory<AppDbContext>>();
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        await context.Database.MigrateAsync(cancellationToken);
    }
}
