using CodeTestWexo.Models;

namespace CodeTestWexo.Interfaces;

public interface ICreditService
{
    Task<List<CastCredits>> GetActorsByMovieIdAsync(int movieId);
    Task<List<CrewCredits>> GetCrewsByMovieIdAsync(int movieId);
}