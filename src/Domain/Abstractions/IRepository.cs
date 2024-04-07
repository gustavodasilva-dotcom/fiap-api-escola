namespace Fiap.Api.Escola.Domain.Abstractions;

public interface IRepository<TEntity>
{
    Task<IEnumerable<TEntity>?> GetAllAsync();

    Task<TEntity?> GetByIdAsync(int Id);

    Task<bool> AddAsync(TEntity entity);

    Task<bool> UpdateAsync(TEntity entity);

    Task<bool> DeleteAsync(TEntity entity);
}
