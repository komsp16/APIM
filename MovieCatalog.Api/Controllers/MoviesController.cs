using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MovieCatalog.Api.Dtos;
using MovieCatalog.Api.Repositories;

namespace MovieCatalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class MoviesController : ControllerBase
{
    private readonly IMovieRepository _repo;

    public MoviesController(IMovieRepository repo) => _repo = repo;

    [HttpGet]
    [SwaggerOperation(Summary = "List all movies")]
    [ProducesResponseType(typeof(IEnumerable<MovieDto>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<MovieDto>> GetAll()
        => Ok(_repo.GetAll().Select(m => new MovieDto(m.Id, m.Title, m.Genre, m.Year)));

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get a movie by id")]
    [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<MovieDto> Get(int id)
    {
        var m = _repo.Get(id);
        return m is null
            ? NotFound(Problem(detail: $"Movie {id} not found", statusCode: 404))
            : Ok(new MovieDto(m.Id, m.Title, m.Genre, m.Year));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new movie")]
    [ProducesResponseType(typeof(MovieDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<MovieDto> Create([FromBody] CreateUpdateMovieDto dto)
    {
        var m = _repo.Add(dto.Title, dto.Genre, dto.Year);
        var result = new MovieDto(m.Id, m.Title, m.Genre, m.Year);
        return CreatedAtAction(nameof(Get), new { id = m.Id }, result);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Update an existing movie")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, [FromBody] CreateUpdateMovieDto dto)
        => _repo.Update(id, dto.Title, dto.Genre, dto.Year) ? NoContent() : NotFound();

    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Delete a movie")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
        => _repo.Delete(id) ? NoContent() : NotFound();
}
