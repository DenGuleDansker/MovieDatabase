using CodeTestWexo.Models.Series;

namespace CodeTestWexo.Interfaces;

public interface ISerieRepository
{
    Task<Serie?> GetSerieDetailsAsync(int serieId);
    Task<SerieTrendingResponse?> GetTrendingSeriesAsync();
}