using System.Data.SqlClient;
using Dapper;
using Fiap.Api.Escola.Domain.Abstractions;
using Fiap.Api.Escola.Domain.Entities;

namespace Fiap.Api.Escola.Infrastructure.Repositories;

internal sealed class AlunoRepository : Repository<Aluno>, IAlunoRepository
{
    public AlunoRepository(SqlConnection connection)
        : base(connection)
    {
    }

    public async Task<IEnumerable<Aluno>?> GetAllAsync()
    {
        return await Connection.QueryAsync<Aluno>(
            @"SELECT [Id] = [a].[id]
                    ,[Nome] = [a].[nome]
                    ,[Usuario] = [a].[usuario],
                    ,[Senha] = [a].[senha]
            FROM [dbo].[aluno] [a];");
    }

    public async Task<Aluno?> GetByIdAsync(int Id)
    {
        return await Connection.QueryFirstOrDefaultAsync<Aluno>(
            @"SELECT [Id] = [a].[id]
                    ,[Nome] = [a].[nome]
                    ,[Usuario] = [a].[usuario],
                    ,[Senha] = [a].[senha]
              FROM [dbo].[aluno] [a];
              WHERE [id] = @id",
            new
            {
                @id = Id
            });
    }
}
