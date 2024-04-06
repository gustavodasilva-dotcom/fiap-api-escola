namespace Fiap.Api.Escola.Domain.Abstractions;

public interface IRepository<TEntity>
{
    Task AddAsync(TEntity entity);

    Task UpdateAsync(TEntity entity);

    Task DeleteAsync(TEntity entity);
}
