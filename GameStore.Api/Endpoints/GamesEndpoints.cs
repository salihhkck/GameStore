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

        group.MapGet("/", async (IGamesRepository repo) =>
        (await repo.GetAllAsync()).Select(game => game.AsDto()));

        group.MapGet("/{id}", async (IGamesRepository repo, int id) =>
        {
            Game? game = await repo.GetAsync(id);
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

            repo.CreateAsync(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });


        group.MapPut("/{id}", async (IGamesRepository repo, int id, UpdateGameDto updatedGameDto) =>
        {
            Game? existingGame = await repo.GetAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            existingGame.Name = updatedGameDto.Name;
            existingGame.Price = updatedGameDto.Price;
            existingGame.ImageUri = updatedGameDto.ImageUri;
            existingGame.Genre = updatedGameDto.Genre;
            existingGame.ReleaseDate = updatedGameDto.ReleaseDate;

            await repo.UpdateAsync(existingGame);
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (IGamesRepository repo, int id) =>
        {
            Game? game = await repo.GetAsync(id);

            if (game is not null)
            {
                await repo.DeleteAsync(id);
            }

            return Results.NoContent();
        });

        return group;
    }
}