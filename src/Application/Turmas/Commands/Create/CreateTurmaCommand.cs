using Fiap.Api.Escola.Domain.Shared;
using MediatR;

namespace Fiap.Api.Escola.Application.Turmas.Commands.Create;

public record CreateTurmaCommand(
    int CursoId,
    string Turma,
    int Ano) : IRequest<Result<bool, Error>>;
