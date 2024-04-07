using Fiap.Api.Escola.Domain.Shared;

namespace Fiap.Api.Escola.Application.Errors;

internal sealed class ApplicationErrors
{
    public readonly static Error AlunoNotFound = new(
        "Aluno.NotFound",
        "Não foi encontrado nenhum aluno com o id informado");

    public readonly static Error TurmaNotFound = new(
        "Turma.NotFound",
        "Não foi encontrada nenhuma turma com o id informado");

    public readonly static Error TurmaMesmoNomeExistente = new(
        "Turma.MesmoNomeExistente",
        "Já existe uma turma cadastrada com o nome informado");
}
