using System.Collections.Concurrent;
using System.Threading;
using MovieCatalog.Api.Models;

namespace MovieCatalog.Api.Repositories;

public sealed class InMemoryMovieRepository : IMovieRepository
{
    private readonly ConcurrentDictionary<int, Movie> _movies = new();
    private int _nextId = 2;

    public InMemoryMovieRepository()
    {
        _movies[1] = new Movie(1, "Inception", "Sci-Fi", 2010);
        _movies[2] = new Movie(2, "The Dark Knight", "Action", 2008);
    }

    public IEnumerable<Movie> GetAll() => _movies.Values.OrderBy(m => m.Id);

    public Movie? Get(int id) => _movies.TryGetValue(id, out var movie) ? movie : null;

    public Movie Add(string title, string genre, int year)
    {
        var id = Interlocked.Increment(ref _nextId);
        var movie = new Movie(id, title, genre, year);
        _movies[id] = movie;
        return movie;
    }

    public bool Update(int id, string title, string genre, int year)
    {
        if (!_movies.TryGetValue(id, out var _)) return false;
        _movies[id] = new Movie(id, title, genre, year);
        return true;
    }

    public bool Delete(int id) => _movies.TryRemove(id, out _);
}
