namespace Mycinema.Application.Models.DTOs;

public class PagedDto<T> where T : class
{
    public int page { get; private set; }
    public List<T> results { get; private set; }
    public int total_pages { get; private set; }
    public int total_results { get; private set; }

    public PagedDto(int page, int totalPages, int totalResults)
    {
        this.page = page;
        this.results = new List<T>();
        this.total_pages = totalPages;
        this.total_results = totalResults;
    }
}
