using System.Reflection;
using GameStore.Api.Entitites;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public class GameStoreDbContext : DbContext
{

    public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options)
    : base(options)
    {

    }

    public DbSet<Game> Games { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}