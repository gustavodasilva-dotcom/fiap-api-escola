using System.Data.SqlClient;
using Fiap.Api.Escola.Domain.Abstractions;
using Fiap.Api.Escola.Domain.Entities;

namespace Fiap.Api.Escola.Infrastructure.Repositories;

internal sealed class AlunoRepository : Repository<Aluno>, IAlunoRepository
{
    public AlunoRepository(SqlConnection connection)
        : base(connection)
    {
    }
}
