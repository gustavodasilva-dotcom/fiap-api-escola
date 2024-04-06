using Fiap.Api.Escola.Domain.Shared;

namespace Fiap.Api.Escola.Application.Errors;

internal sealed class ApplicationErrors
{
    public readonly static Error AlunoNotFound = new(
        "Aluno.NotFound",
        "Não foi encontrado nenhum aluno com o id informado");
}
