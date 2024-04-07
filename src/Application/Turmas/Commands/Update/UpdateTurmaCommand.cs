using Fiap.Api.Escola.Application.Contracts.Responses;
using Fiap.Api.Escola.Domain.Shared;
using MediatR;

namespace Fiap.Api.Escola.Application.Turmas.Commands.Update;

public record UpdateTurmaCommand(
    int Id,
    int CursoId,
    string Turma,
    int Ano) : IRequest<Result<TurmaResponse, Error>>;
