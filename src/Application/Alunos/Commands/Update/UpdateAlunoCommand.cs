using Fiap.Api.Escola.Application.Contracts.Responses;
using Fiap.Api.Escola.Domain.Shared;
using MediatR;

namespace Fiap.Api.Escola.Application.Alunos.Commands.Update;

public record UpdateAlunoCommand(
    int Id,
    string Nome,
    string Usuario,
    string Senha) : IRequest<Result<AlunoResponse, Error>>;
