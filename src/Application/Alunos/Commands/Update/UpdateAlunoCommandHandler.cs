using Fiap.Api.Escola.Application.Errors;
using Fiap.Api.Escola.Domain.Abstractions;
using Fiap.Api.Escola.Domain.Entities;
using Fiap.Api.Escola.Domain.Shared;
using MediatR;

namespace Fiap.Api.Escola.Application.Alunos.Commands.Update;

internal sealed class UpdateAlunoCommandHandler
    : IRequestHandler<UpdateAlunoCommand, Result<Aluno, Error>>
{
    private readonly IAlunoRepository _alunoRepository;

    public UpdateAlunoCommandHandler(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    public async Task<Result<Aluno, Error>> Handle(
        UpdateAlunoCommand request,
        CancellationToken cancellationToken)
    {
        var aluno = await _alunoRepository.GetByIdAsync(request.Id);

        if (aluno is null)
        {
            return ApplicationErrors.AlunoNotFound;
        }

        var atualizarAlunoResult = Aluno.Update(
            request.Id,
            request.Nome,
            request.Usuario,
            request.Senha);

        await _alunoRepository.UpdateAsync(atualizarAlunoResult.Value!);

        return atualizarAlunoResult.Value!;
    }
}
