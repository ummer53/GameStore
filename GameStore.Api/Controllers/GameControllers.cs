using System;
using GameStore.Api.Dtos;

namespace GameStore.Api.Controllers;

public static class GameControllers
{
    static List<GameDto> games =
        [
            new GameDto(1, "Game 1", "Action", 59.99m, new DateOnly(2023, 1, 1)),
            new GameDto(2, "Game 2", "Adventure", 49.99m, new DateOnly(2023, 2, 1)),
            new GameDto(3, "Game 3", "RPG", 39.99m, new DateOnly(2023, 3, 1))
        ];

    public static RouteGroupBuilder MapGameControllers(this WebApplication app)
    {
        var group = app.MapGroup("games")
                        .WithParameterValidation();

        group.MapGet("/", () =>
        {
            return Results.Ok(games);
        });

        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(game => game.Id == id);
            return game != null ? Results.Ok(game) : Results.NotFound("Game not found");
        }).WithName("GetGameById");

        group.MapPost("/", (CreateGameDto gameDto) =>
        {
            var newGame = new GameDto(games.Count + 1, gameDto.Name, gameDto.Genre, gameDto.Price, gameDto.ReleaseDate);
            games.Add(newGame);
            return Results.CreatedAtRoute("GetGameById", new { id = newGame.Id });
        });

        group.MapPut("/{id}", (int id, CreateGameDto gameDto) =>
        {
            var gameIndex = games.FindIndex(game => game.Id == id);
            if (gameIndex == -1)
            {
                return Results.NotFound("Game not found");
            }

            var game = new GameDto(id, gameDto.Name, gameDto.Genre, gameDto.Price, gameDto.ReleaseDate);
            games[gameIndex] = game;
            return Results.Ok(game);
        });

        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });

        return group;

    }

}