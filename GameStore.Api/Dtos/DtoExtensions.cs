using System;
using GameStore.Api.Entities;

namespace GameStore.Api.Dtos;

public static class DtoExtensions
{
    public static GameDto ToDto(this Game? game)
    {
        if (game == null)
        {
            return null!;
        }
        return new GameDto(
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate);
    }

    public static Game ToEntity(this CreateGameDto dto)
    {
        return new Game
        {
            Name = dto.Name,
            GenreId = dto.GenreId,
            Price = dto.Price,
            ReleaseDate = dto.ReleaseDate
        };
    }

    public static Game ToEntityFromUpdateDto(this UpdateGameDto dto, Game? game)
    {
        if (game == null)
        {
            return null!;
        }
        game.Name = dto.Name;
        game.GenreId = dto.GenreId;
        game.Price = dto.Price;
        game.ReleaseDate = dto.ReleaseDate;
        return game;
    }

}
