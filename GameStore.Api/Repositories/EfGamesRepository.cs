using GameStore.Api.Data;
using GameStore.Api.Entitites;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Repositories;

public class EfGamesRepository : IGamesRepository
{
    private readonly GameStoreDbContext _dbcontext;

    public EfGamesRepository(GameStoreDbContext context)
    {
        _dbcontext = context;
    }

    public async Task CreateAsync(Game game)
    {
        _dbcontext.Games.Add(game);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _dbcontext.Games.Where(game => game.Id == id)
                        .ExecuteDeleteAsync();
    }

    public async Task<Game?> GetAsync(int id)
    {
        return await _dbcontext.Games.FindAsync(id);
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await _dbcontext.Games.AsNoTracking().ToListAsync();
    }

    public async Task UpdateAsync(Game updatedGame)
    {
        _dbcontext.Update(updatedGame);
        await _dbcontext.SaveChangesAsync();
    }
}