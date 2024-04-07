using Fiap.Api.Escola.Domain.Entities;

namespace Fiap.Api.Escola.Domain.Abstractions;

public interface ITurmaRepository : IRepository<Turma>
{
    Task<Turma?> GetTurmaMesmoNomeAsync(string nome);
}
