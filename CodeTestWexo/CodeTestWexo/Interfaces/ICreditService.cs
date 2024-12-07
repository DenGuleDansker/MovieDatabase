using CodeTestWexo.Components.Models;

namespace CodeTestWexo.Interfaces;

public interface ICreditService
{
    Task<List<Actor>> GetActorsByMovieIdAsync(int movieId);
}