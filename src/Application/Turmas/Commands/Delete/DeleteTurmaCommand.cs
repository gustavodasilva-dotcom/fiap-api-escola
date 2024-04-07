using Fiap.Api.Escola.Application.Contracts.Responses;
using Fiap.Api.Escola.Domain.Shared;
using MediatR;

namespace Fiap.Api.Escola.Application.Turmas.Commands.Delete;

public record DeleteTurmaCommand(int Id) : IRequest<Result<TurmaResponse, Error>>;
