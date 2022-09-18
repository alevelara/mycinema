using System.Text.Json.Serialization;

namespace Mycinema.Application.Models.DTOs;

public class PagedDto<T> where T : class
{
    public int page { get; set; }
    public T[] results { get; set; } = new T[0];
    public int total_pages { get; set; }
    public int total_results { get; set; }    
}
