using Fiap.Api.Escola.Domain.Shared;

namespace Fiap.Api.Escola.Domain.Errors;

internal sealed class DomainErrors
{
    public readonly static Error TurmaAnoInvalido = new(
        "Turma.AnoInvalido",
        "O ano informado para a turma não pode ser menor que o ano atual");
}
