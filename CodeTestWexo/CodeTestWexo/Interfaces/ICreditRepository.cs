using CodeTestWexo.Models;

namespace CodeTestWexo.Interfaces;

public interface ICreditRepository
{
    Task<List<CastCredits>> GetActorsByMovieIdAsync(int movieId);
    Task<List<CrewCredits>> GetCrewsByMovieIdAsync(int movieId);
    
    Task<List<CastCredits>> GetActorsBySeriesIdAsync(int seriesId);
    Task<List<CrewCredits>> GetCrewsBySeriesIdAsync(int seriesId);
}