using Fiap.Api.Escola.Domain.Entities;
using Fiap.Api.Escola.Domain.Shared;
using MediatR;

namespace Fiap.Api.Escola.Application.Alunos.Commands.Update;

public record UpdateAlunoCommand(
    int Id,
    string Nome,
    string Usuario,
    string Senha) : IRequest<Result<Aluno, Error>>;
