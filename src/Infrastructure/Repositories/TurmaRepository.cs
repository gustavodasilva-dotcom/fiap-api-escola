using Fiap.Api.Escola.Domain.Abstractions;
using Fiap.Api.Escola.Domain.Entities;
using System.Data.SqlClient;

namespace Fiap.Api.Escola.Infrastructure.Repositories;

internal sealed class TurmaRepository : Repository<Turma>, ITurmaRepository
{
    public TurmaRepository(SqlConnection connection)
        : base(connection)
    {
    }
}
