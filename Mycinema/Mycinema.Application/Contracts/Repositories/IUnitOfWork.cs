using Mycinema.Domain.Common;
using System.Data.Common;

namespace Mycinema.Application.Contracts.Repositories;

public interface IUnitOfWork : IDisposable
{    
    Task BeginAsync();    
    Task CommitAsync();    
    Task RollbackAsync();
    IAsyncReadRepository<TEntity> ReadRepository<TEntity>() where TEntity : BaseDomainModel;    
}
