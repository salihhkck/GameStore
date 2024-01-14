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

    public void Create(Game game)
    {
        _dbcontext.Games.Add(game);
        _dbcontext.SaveChanges();
    }

    public void Delete(int id)
    {
        _dbcontext.Games.Where(game => game.Id == id)
                        .ExecuteDelete();
    }

    public Game? Get(int id)
    {
        return _dbcontext.Games.Find(id);
    }

    public IEnumerable<Game> GetAll()
    {
        return _dbcontext.Games.ToList();
    }

    public void Update(Game updatedGame)
    {
        _dbcontext.Update(updatedGame);
        _dbcontext.SaveChanges();
    }
}