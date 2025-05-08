using System;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>()
        .ToTable("genre")
            .HasData(
            new Genre { Id = 1, Name = "Action" },
            new Genre { Id = 2, Name = "Adventure" },
            new Genre { Id = 3, Name = "RPG" },
            new Genre { Id = 4, Name = "Simulation" },
            new Genre { Id = 5, Name = "Strategy" },
            new Genre { Id = 6, Name = "Sports" },
            new Genre { Id = 7, Name = "Puzzle" },
            new Genre { Id = 8, Name = "Horror" },
            new Genre { Id = 9, Name = "Platformer" },
            new Genre { Id = 10, Name = "Racing" }
        );

        modelBuilder.Entity<Game>().ToTable("games").HasData(
            new Game { Id = 1, Name = "Game 1", GenreId = 1, Price = 59.99m, ReleaseDate = new DateOnly(2023, 1, 1) },
            new Game { Id = 2, Name = "Game 2", GenreId = 2, Price = 49.99m, ReleaseDate = new DateOnly(2023, 2, 1) },
            new Game { Id = 3, Name = "Game 3", GenreId = 3, Price = 39.99m, ReleaseDate = new DateOnly(2023, 3, 1) },
            new Game { Id = 4, Name = "Game 4", GenreId = 4, Price = 29.99m, ReleaseDate = new DateOnly(2023, 4, 1) },
            new Game { Id = 5, Name = "Game 5", GenreId = 5, Price = 19.99m, ReleaseDate = new DateOnly(2023, 5, 1) },
            new Game { Id = 6, Name = "Game 6", GenreId = 6, Price = 9.99m, ReleaseDate = new DateOnly(2023, 6, 1) }
        );
    }

}
