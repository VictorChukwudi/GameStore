using GameStore.Api.Endpoints;
using GameStore.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IGameRepository, InMemRepository>();
var app = builder.Build();

app.MapGamesEndpoints();
app.Run();
