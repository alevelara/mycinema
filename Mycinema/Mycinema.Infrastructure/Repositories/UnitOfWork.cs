using Mycinema.Application.Contracts.Repositories;
using Mycinema.Domain.Common;
using Mycinema.Infrastructure.Repositories.Read;
using System.Collections;
using System.Data.Common;

namespace Mycinema.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private Hashtable _readRepostories;    

    private readonly DbConnection _connection;
    private DbTransaction _transaction;

    public UnitOfWork(DbConnection connection)
    {
        _connection = connection;
    }

    public DbConnection Connection => _connection;

    public DbTransaction Transaction => _transaction;
   
    public async Task BeginAsync()
    {
        _transaction = await _connection.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _transaction.CommitAsync();
    }

    public void Dispose()
    {
        if (_transaction != null)
            _transaction.Dispose();

        _transaction = null;
    }      

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
    }
    
    public IAsyncReadRepository<TEntity> ReadRepository<TEntity>() where TEntity : BaseDomainModel
    {
        if (_readRepostories == null)
            _readRepostories = new Hashtable();

        var nameType = typeof(TEntity).Name;

        if (!_readRepostories.ContainsKey(nameType))
        {
            var repositoryType = typeof(GenericReadRepository<TEntity>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _transaction);
            _readRepostories.Add(nameType, repositoryInstance);
        }

        return (IAsyncReadRepository<TEntity>)_readRepostories[nameType];
    }
}
