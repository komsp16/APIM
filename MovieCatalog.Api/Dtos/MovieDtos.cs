using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Api.Dtos;

public sealed record MovieDto(int Id, string Title, string Genre, int Year);

public sealed class CreateUpdateMovieDto
{
    [Required, StringLength(100)]
    public string Title { get; init; } = default!;

    [Required, StringLength(50)]
    public string Genre { get; init; } = default!;

    [Range(1888, 2100)]
    public int Year { get; init; }
}
