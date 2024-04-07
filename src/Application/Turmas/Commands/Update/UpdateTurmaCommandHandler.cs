using AutoMapper;
using Fiap.Api.Escola.Application.Contracts.Responses;
using Fiap.Api.Escola.Application.Errors;
using Fiap.Api.Escola.Domain.Abstractions;
using Fiap.Api.Escola.Domain.Entities;
using Fiap.Api.Escola.Domain.Shared;
using MediatR;

namespace Fiap.Api.Escola.Application.Turmas.Commands.Update;

internal sealed class UpdateTurmaCommandHandler
    : IRequestHandler<UpdateTurmaCommand, Result<TurmaResponse, Error>>
{
    private readonly ITurmaRepository _turmaRepository;
    private readonly IMapper _mapper;

    public UpdateTurmaCommandHandler(
        ITurmaRepository turmaRepository,
        IMapper mapper)
    {
        _turmaRepository = turmaRepository;
        _mapper = mapper;
    }

    public async Task<Result<TurmaResponse, Error>> Handle(
        UpdateTurmaCommand request,
        CancellationToken cancellationToken)
    {
        var turma = await _turmaRepository.GetByIdAsync(request.Id);

        if (turma is null)
        {
            return ApplicationErrors.TurmaNotFound;
        }

        var atualizarTurmaResult = Turma.Update(
            request.Id,
            request.CursoId,
            request.Turma,
            request.Ano);

        await _turmaRepository.UpdateAsync(atualizarTurmaResult.Value!);

        return _mapper.Map<TurmaResponse>(atualizarTurmaResult.Value!);
    }
}
