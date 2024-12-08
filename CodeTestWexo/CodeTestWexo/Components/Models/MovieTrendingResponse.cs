namespace CodeTestWexo.Components.Models;

public class MovieTrendingResponse
{
    public List<Movie> Results { get; set; }
    public int TotalResults { get; set; }
    public int TotalPages { get; set; }
}