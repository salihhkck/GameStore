using GameStore.Api.Dtos;

namespace GameStore.Api.Entitites;

public static class EntityExtensions
{
    public static GameDto AsDto(this Game game)
    {
        return new GameDto(
            game.Id,
            game.Genre,
            game.Name,
            game.Price,
            game.ReleaseDate,
            game.ImageUri
        );
    }
}