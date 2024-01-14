using GameStore.Api.Entitites;

namespace GameStore.Api.Repositories;

public class InMemoryGamesRepo : IGamesRepository
{
    private readonly List<Game> games = new()
{
    new Game()
    {
        Id=1,
        Name="Süper Mario 3",
        Genre="Macera",
        Price=25.99M,
        ReleaseDate= new DateTime(2012,5,21),
        ImageUri="https://placehold.co/100"
    },
    new Game()
    {
        Id=2,
        Name="Fifa 24",
        Genre="Spor",
        Price=45.99M,
        ReleaseDate= new DateTime(2023,8,15),
        ImageUri="https://placehold.co/100"
    },
    new Game()
    {
        Id=3,
        Name="Harry Potter Legacy",
        Genre="Macera",
        Price=59.99M,
        ReleaseDate= new DateTime(2023,6,14),
        ImageUri="https://placehold.co/100"
    }
};

    public IEnumerable<Game> GetAllAsync()
    {
        return games;
    }

    public Game? GetAsync(int id)
    {
        return games.Find(game => game.Id == id);
    }

    public void CreateAsync(Game game)
    {
        game.Id = games.Max(game => game.Id) + 1;
        games.Add(game);
    }

    public void UpdateAsync(Game updatedGame)
    {
        var index = games.FindIndex(game => game.Id == updatedGame.Id);
        games[index] = updatedGame;
    }

    public void DeleteAsync(int id)
    {
        var index = games.FindIndex(game => game.Id == id);
        games.RemoveAt(index);
    }

    Task IGamesRepository.CreateAsync(Game game)
    {
        throw new NotImplementedException();
    }

    Task IGamesRepository.DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task<Game?> IGamesRepository.GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<Game>> IGamesRepository.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    Task IGamesRepository.UpdateAsync(Game updatedGame)
    {
        throw new NotImplementedException();
    }
}