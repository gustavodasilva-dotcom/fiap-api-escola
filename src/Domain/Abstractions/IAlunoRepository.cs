using Fiap.Api.Escola.Domain.Entities;

namespace Fiap.Api.Escola.Domain.Abstractions;

public interface IAlunoRepository : IRepository<Aluno>
{
    Task<IEnumerable<Aluno>?> GetAllAsync();

    Task<Aluno?> GetByIdAsync(int Id);
}
