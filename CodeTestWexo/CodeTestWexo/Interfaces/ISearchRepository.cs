using CodeTestWexo.Models.Search;

namespace CodeTestWexo.Interfaces;

public interface ISearchRepository
{
    Task<SearchResponse?> GetSearchDetailsAsync(string query);

}