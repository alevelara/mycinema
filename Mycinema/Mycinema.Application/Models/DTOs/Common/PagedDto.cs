using System.Text.Json.Serialization;

namespace Mycinema.Application.Models.DTOs;

public class PagedDto<T> where T : class
{
    public int page { get; private set; }
    public T[] results { get; private set; } = new T[0];
    public int total_pages { get; private set; }
    public int total_results { get; private set; }

    [JsonConstructor]
    public PagedDto(int page, T[] results, int totalPages, int totalResults)
    {
        this.page = page;
        this.results = results;
        this.total_pages = totalPages;
        this.total_results = totalResults;
    }

    [JsonConstructor]
    public PagedDto()
    {        
    }
}
