using Dapper;
using System.Data.SqlClient;
using Fiap.Api.Escola.Domain.Primitives;

namespace Fiap.Api.Escola.Infrastructure.Repositories;

internal abstract class Repository<TEntity>
    where TEntity : Entity
{
    protected readonly SqlConnection Connection;

    protected Repository(SqlConnection connection)
    {
        Connection = connection;
    }
    
    public async Task AddAsync(TEntity entity)
    {
        await Connection.ExecuteAsync(entity.ToInsertQuery());
    }

    public async Task UpdateAsync(TEntity entity)
    {
        await Connection.ExecuteAsync(entity.ToUpdateQuery());
    }

    public async Task DeleteAsync(TEntity entity)
    {
        await Connection.ExecuteAsync(entity.ToDeleteQuery());
    }
}
