using MovieCatalog.Api.Models;

namespace MovieCatalog.Api.Repositories;

public interface IMovieRepository
{
    IEnumerable<Movie> GetAll();
    Movie? Get(int id);
    Movie Add(string title, string genre, int year);
    bool Update(int id, string title, string genre, int year);
    bool Delete(int id);
}
