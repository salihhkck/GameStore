using GameStore.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static void InitializeDb(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreDbContext>();
        dbContext.Database.Migrate();
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