using Mycinema.Domain.Entities;

namespace Mycinema.Application.Contracts.Repositories;

public interface IMovieReadRepository
{
    Task<List<Movie>> GetMostSuccesfulMoviesByDate(DateTime startdatetime, DateTime endDatetime);
}
