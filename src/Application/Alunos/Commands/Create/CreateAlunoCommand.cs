using Fiap.Api.Escola.Domain.Entities;
using Fiap.Api.Escola.Domain.Shared;
using MediatR;

namespace Fiap.Api.Escola.Application.Alunos.Commands.Create;

public record CreateAlunoCommand(
    string Nome,
    string Usuario,
    string Senha) : IRequest<Result<Aluno, Error>>;
