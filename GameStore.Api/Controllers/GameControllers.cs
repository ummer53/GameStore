using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Controllers;

public static class GameControllers
{

    public static RouteGroupBuilder MapGameControllers(this WebApplication app)
    {
        var group = app.MapGroup("games")
                        .WithParameterValidation();

        group.MapGet("/", (GameStoreContext dbContext) =>
        {
            var games = dbContext.Games.Include(g => g.Genre).ToList();
            var gamesResponse = games.Select(g => new GameDto(
                g.Id,
                g.Name,
                g.Genre!.Name,
                g.Price,
                g.ReleaseDate)).ToList();
            return Results.Ok(gamesResponse);
        });

        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var game = await dbContext.Games.Include(g => g.Genre)
                .FirstOrDefaultAsync(g => g.Id == id);

            var gameResponse = DtoExtensions.ToDto(game);

            return gameResponse != null ? Results.Ok(gameResponse) : Results.NotFound("Game not found");
        }).WithName("GetGameById");

        group.MapPost("/", async (CreateGameDto gameDto, GameStoreContext dbContext) =>
        {
            var newGame = DtoExtensions.ToEntity(gameDto);
            await dbContext.Games.AddAsync(newGame);
            await dbContext.SaveChangesAsync();
            return Results.CreatedAtRoute("GetGameById", new { id = newGame.Id });
        });

        group.MapPut("/{id}", async (int id, UpdateGameDto gameDto, GameStoreContext dbContext) =>
        {
            await dbContext.Games
                .Where(g => g.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(g => g.Name, gameDto.Name)
                    .SetProperty(g => g.Price, gameDto.Price)
                    .SetProperty(g => g.ReleaseDate, gameDto.ReleaseDate)
                    .SetProperty(g => g.GenreId, gameDto.GenreId));
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id, GameStoreContext dbContext) =>
        {
            var game = dbContext.Games.Where(g => g.Id == id).ExecuteDelete();
            return Results.NoContent();
        });

        return group;

    }

}