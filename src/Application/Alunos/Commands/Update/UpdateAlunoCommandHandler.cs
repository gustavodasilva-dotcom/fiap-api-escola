using AutoMapper;
using Fiap.Api.Escola.Application.Contracts.Responses;
using Fiap.Api.Escola.Application.Errors;
using Fiap.Api.Escola.Domain.Abstractions;
using Fiap.Api.Escola.Domain.Shared;
using MediatR;

namespace Fiap.Api.Escola.Application.Alunos.Commands.Update;

internal sealed class UpdateAlunoCommandHandler
    : IRequestHandler<UpdateAlunoCommand, Result<AlunoResponse, Error>>
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IMapper _mapper;

    public UpdateAlunoCommandHandler(
        IAlunoRepository alunoRepository,
        IMapper mapper)
    {
        _alunoRepository = alunoRepository;
        _mapper = mapper;
    }

    public async Task<Result<AlunoResponse, Error>> Handle(
        UpdateAlunoCommand request,
        CancellationToken cancellationToken)
    {
        var aluno = await _alunoRepository.GetByIdAsync(request.Id);

        if (aluno is null)
        {
            return ApplicationErrors.AlunoNotFound;
        }

        var atualizarAlunoResult = Domain.Entities.Aluno.Update(
            request.Id,
            request.Nome,
            request.Usuario,
            request.Senha);

        await _alunoRepository.UpdateAsync(atualizarAlunoResult.Value!);

        return _mapper.Map<AlunoResponse>(atualizarAlunoResult.Value!);
    }
}
