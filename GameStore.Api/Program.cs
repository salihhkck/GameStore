using GameStore.Api.Entitites;

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

app.MapGet("/games", () => games);

app.Run();
