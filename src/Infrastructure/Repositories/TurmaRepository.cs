using Dapper;
using Fiap.Api.Escola.Domain.Abstractions;
using Fiap.Api.Escola.Domain.Entities;
using Fiap.Api.Escola.Infrastructure.Queries;
using System.Data.SqlClient;

namespace Fiap.Api.Escola.Infrastructure.Repositories;

internal sealed class TurmaRepository : Repository<Turma>, ITurmaRepository
{
    public TurmaRepository(SqlConnection connection)
        : base(connection)
    {
    }

    public async Task<Turma?> GetTurmaMesmoNomeAsync(string nome)
    {
        return await Connection.QueryFirstOrDefaultAsync<Turma>(
            TurmaQueries.QueryExisteTurmaMesmoNome, new { nome });
    }
}
