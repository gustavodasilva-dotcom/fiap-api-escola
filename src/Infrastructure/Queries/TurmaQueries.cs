namespace Fiap.Api.Escola.Infrastructure.Queries;

internal static class TurmaQueries
{
    internal const string QueryExisteTurmaMesmoNome =
        @"SELECT * FROM [dbo].[turma] WHERE [turma] = @nome;";
}
