using GameStore.Api.Entities;
namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string getGameEndpointName = "GetGame";

    static List<Game> games = new(){
            new Game(){
                Id = 1,
                Name= "Street Fighter",
                Genre= "Action",
                Price= 19.99M,
                ReleaseDate=new DateTime(1991, 2, 1),
                ImageUrl="https://placehold.co/100"
            },
            new Game(){
                Id = 2,
                Name= "Final Fantasy XIV",
                Genre= "Role Playing",
                Price= 59.99M,
                ReleaseDate=new DateTime(2010, 9, 10),
                ImageUrl="https://placehold.co/100"
            },
            new Game(){
                Id = 3,
                Name= "FIFA 23",
                Genre= "Sports",
                Price= 69.99M,
                ReleaseDate=new DateTime(2022, 9, 27),
                ImageUrl="https://placehold.co/100"
            },
        };
    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {
        //route group - similar to route groups in expressjs
        var group = routes.MapGroup("/games")
                        .WithParameterValidation();

        group.MapGet("/", () => games);

        group.MapGet("/{id}", (int id) =>
        {
            Game? game = games.Find(game => game.Id == id);
            if (game is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(game);
        }).WithName(getGameEndpointName);

        group.MapPost("/", (Game game) =>
        {
            game.Id = games.Max(game => game.Id) + 1;
            games.Add(game);
            Console.WriteLine(games.Count);

            return Results.CreatedAtRoute(getGameEndpointName, new { id = game.Id }, game);
        });

        group.MapPut("/{id}", (int id, Game updatedGame) =>
        {
            Game? existingGame = games.Find(game => game.Id == id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }

            existingGame.Name = updatedGame.Name;
            existingGame.Genre = updatedGame.Genre;
            existingGame.ReleaseDate = updatedGame.ReleaseDate;
            existingGame.Price = updatedGame.Price;
            existingGame.ImageUrl = updatedGame.ImageUrl;

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

        return group;
    }
}
