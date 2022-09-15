using Mycinema.Domain.Common;

namespace Mycinema.Application.Contracts.Repositories;

public interface IAsyncReadRepository<T> where T : BaseDomainModel
{
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
}
