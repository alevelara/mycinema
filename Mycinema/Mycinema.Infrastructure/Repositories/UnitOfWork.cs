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
}
