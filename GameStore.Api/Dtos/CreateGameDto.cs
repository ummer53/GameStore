using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class CreateGameDto
(
    [Required][MaxLength(50)] string Name,
    [Required] string Genre,
    [Required][Range(1, 100)] decimal Price,
    DateOnly ReleaseDate = default
);

