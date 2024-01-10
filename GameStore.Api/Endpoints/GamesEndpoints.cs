using GameStore.Api.Dtos;
using GameStore.Api.Entitites;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";



    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/games")
                .WithParameterValidation();

        group.MapGet("/", (IGamesRepository repo) =>
        repo.GetAll().Select(game => game.AsDto()));

        group.MapGet("/{id}", (IGamesRepository repo, int id) =>
        {
            Game? game = repo.Get(id);
            return game is not null ? Results.Ok(game.AsDto()) : Results.NotFound();
        })
        .WithName(GetGameEndpointName);

        group.MapPost("/", (IGamesRepository repo, CreateGameDto gameDto) =>
        {
            Game game = new()
            {
                Name = gameDto.Name,
                Genre = gameDto.Genre,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                ImageUri = gameDto.ImageUri
            };

            repo.Create(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });


        group.MapPut("/{id}", (IGamesRepository repo, int id, UpdateGameDto updatedGameDto) =>
        {
            Game? existingGame = repo.Get(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            existingGame.Name = updatedGameDto.Name;
            existingGame.Price = updatedGameDto.Price;
            existingGame.ImageUri = updatedGameDto.ImageUri;
            existingGame.Genre = updatedGameDto.Genre;
            existingGame.ReleaseDate = updatedGameDto.ReleaseDate;

            repo.Update(existingGame);
            return Results.NoContent();
        });

        group.MapDelete("/{id}", (IGamesRepository repo, int id) =>
        {
            Game? game = repo.Get(id);

            if (game is not null)
            {
                repo.Delete(id);
            }

            return Results.NoContent();
        });

        return group;
    }
}