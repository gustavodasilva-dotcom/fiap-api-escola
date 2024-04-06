using Fiap.Api.Escola.Domain.Abstractions;
using Fiap.Api.Escola.Domain.Entities;
using Fiap.Api.Escola.Domain.Shared;
using MediatR;

namespace Fiap.Api.Escola.Application.Alunos.Commands.Create;

internal sealed class CreateAlunoCommandHandler
    : IRequestHandler<CreateAlunoCommand, Result<Aluno, Error>>
{
    private readonly IAlunoRepository _alunoRepository;

    public CreateAlunoCommandHandler(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    public async Task<Result<Aluno, Error>> Handle(
        CreateAlunoCommand request,
        CancellationToken cancellationToken)
    {
        var criarAlunoResult = Aluno.Create(
            request.Nome,
            request.Usuario,
            request.Senha);

        if (criarAlunoResult.IsFailure)
        {
            return criarAlunoResult.Error!;
        }

        await _alunoRepository.AddAsync(criarAlunoResult.Value!);

        return criarAlunoResult.Value!;
    }
}
