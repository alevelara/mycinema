using Mycinema.Domain.Common;

namespace Mycinema.Application.Contracts.Repositories;

public interface IAsyncWriteRepository<T> where T : BaseDomainModel
{
    Task<int> AddAsync(T entity);
    Task<int> UpdateAsync(T entity);
    Task<int> DeleteAsync(int id);
}
