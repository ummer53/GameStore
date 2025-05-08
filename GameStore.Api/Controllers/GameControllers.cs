using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

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

        group.MapGet("/", (GameStoreContext dbContext) =>
        {
            var games = dbContext.Games.Include(g => g.Genre).ToList();
            return Results.Ok(games);
        });

        group.MapGet("/{id}", (int id, GameStoreContext dbContext) =>
        {
            var game = dbContext.Games.Include(g => g.Genre)
                .FirstOrDefault(g => g.Id == id);

            return game != null ? Results.Ok(game) : Results.NotFound("Game not found");
        }).WithName("GetGameById");

        group.MapPost("/", (CreateGameDto gameDto, GameStoreContext dbContext) =>
        {
            var newGame = new Game
            {
                Name = gameDto.Name,
                GenreId = gameDto.GenreId,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate
            };
            dbContext.Games.Add(newGame);
            dbContext.SaveChanges();
            return Results.CreatedAtRoute("GetGameById", new { id = newGame.Id });
        });

        group.MapPut("/{id}", (int id, CreateGameDto gameDto, GameStoreContext dbContext) =>
        {
            var game = dbContext.Games
            .Include(g => g.Genre)
            .FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return Results.NotFound("Game not found");
            }
            game.Name = gameDto.Name;
            game.GenreId = gameDto.GenreId;
            game.Price = gameDto.Price;
            game.ReleaseDate = gameDto.ReleaseDate;
            dbContext.Games.Update(game);
            dbContext.SaveChanges();
            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id, GameStoreContext dbContext) =>
        {
            var game = dbContext.Games.Include(g => g.Genre)
                .FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return Results.NotFound("Game not found");
            }
            dbContext.Games.Remove(game);
            dbContext.SaveChanges();
            return Results.NoContent();
        });

        return group;

    }

}