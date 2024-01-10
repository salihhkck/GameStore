using GameStore.Api.Endpoints;
using GameStore.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IGamesRepository, InMemoryGamesRepo>();

var connectionString = builder.Configuration.GetConnectionString("GameStoreContext");
var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
