using Fiap.Api.Escola.Application.Errors;
using Fiap.Api.Escola.Domain.Abstractions;
using Fiap.Api.Escola.Domain.Entities;
using Fiap.Api.Escola.Domain.Shared;
using MediatR;

namespace Fiap.Api.Escola.Application.Alunos.Commands.Delete;

internal sealed class DeleteAlunoCommandHandler
    : IRequestHandler<DeleteAlunoCommand, Result<Aluno, Error>>
{
    private readonly IAlunoRepository _alunoRepository;

    public DeleteAlunoCommandHandler(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    public async Task<Result<Aluno, Error>> Handle(
        DeleteAlunoCommand request,
        CancellationToken cancellationToken)
    {
        var aluno = await _alunoRepository.GetByIdAsync(request.Id);

        if (aluno is null)
        {
            return ApplicationErrors.AlunoNotFound;
        }

        await _alunoRepository.DeleteAsync(aluno);

        return aluno;
    }
}
