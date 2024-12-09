using CodeTestWexo.Models;

namespace CodeTestWexo.Interfaces;

public interface ICreditRepository
{
    Task<List<CastCredits>> GetActorsByMovieIdAsync(int movieId);
    Task<List<CrewCredits>> GetCrewsByMovieIdAsync(int movieId);
}