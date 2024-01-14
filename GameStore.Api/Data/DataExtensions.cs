using GameStore.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static async Task InitializeDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("GameStoreContext");
        services.AddSqlServer<GameStoreDbContext>(connectionString)
        .AddScoped<IGamesRepository, EfGamesRepository>();

        return services;
    }
}