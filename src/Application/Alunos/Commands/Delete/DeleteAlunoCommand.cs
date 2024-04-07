using Fiap.Api.Escola.Application.Contracts.Responses;
using Fiap.Api.Escola.Domain.Shared;
using MediatR;

namespace Fiap.Api.Escola.Application.Alunos.Commands.Delete;

public record DeleteAlunoCommand(int Id) : IRequest<Result<AlunoResponse, Error>>;
