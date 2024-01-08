using GameStore.Api.Entitites;

const string GetGameEndpointName = "GetGame";

List<Game> games = new()
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

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var group = app.MapGroup("/games");

group.MapGet("/", () => games);

group.MapGet("/{id}", (int id) =>
{
    Game? game = games.Find(game => game.Id == id);

    if (game is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(game);
})
.WithName(GetGameEndpointName);

group.MapPost("/", (Game game) =>
{
    game.Id = games.Max(game => game.Id) + 1;
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});


group.MapPut("/{id}", (int id, Game updatedGame) =>
{
    Game? existingGame = games.Find(game => game.Id == id);

    if (existingGame is null)
    {
        return Results.NotFound();
    }

    existingGame.Name = updatedGame.Name;
    existingGame.Price = updatedGame.Price;
    existingGame.ImageUri = updatedGame.ImageUri;
    existingGame.Genre = updatedGame.Genre;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;

    return Results.NoContent();
});

group.MapDelete("/{id}", (int id) =>
{
    Game? game = games.Find(game => game.Id == id);

    if (game is not null)
    {
        games.Remove(game);
    }

    return Results.NoContent();
});


app.Run();
