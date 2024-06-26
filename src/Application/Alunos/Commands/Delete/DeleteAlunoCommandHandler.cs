using AutoMapper;
using Fiap.Api.Escola.Application.Contracts.Responses;
using Fiap.Api.Escola.Application.Errors;
using Fiap.Api.Escola.Domain.Abstractions;
using Fiap.Api.Escola.Domain.Shared;
using MediatR;

namespace Fiap.Api.Escola.Application.Alunos.Commands.Delete;

internal sealed class DeleteAlunoCommandHandler
    : IRequestHandler<DeleteAlunoCommand, Result<AlunoResponse, Error>>
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IMapper _mapper;

    public DeleteAlunoCommandHandler(
        IAlunoRepository alunoRepository,
        IMapper mapper)
    {
        _alunoRepository = alunoRepository;
        _mapper = mapper;
    }

    public async Task<Result<AlunoResponse, Error>> Handle(
        DeleteAlunoCommand request,
        CancellationToken cancellationToken)
    {
        var aluno = await _alunoRepository.GetByIdAsync(request.Id);

        if (aluno is null)
        {
            return ApplicationErrors.AlunoNotFound;
        }

        await _alunoRepository.DeleteAsync(aluno);

        return _mapper.Map<AlunoResponse>(aluno);
    }
}
