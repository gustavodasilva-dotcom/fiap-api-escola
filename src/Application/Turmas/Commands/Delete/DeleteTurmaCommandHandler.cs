using AutoMapper;
using Fiap.Api.Escola.Application.Contracts.Responses;
using Fiap.Api.Escola.Application.Errors;
using Fiap.Api.Escola.Domain.Abstractions;
using Fiap.Api.Escola.Domain.Shared;
using MediatR;

namespace Fiap.Api.Escola.Application.Turmas.Commands.Delete;

internal sealed class DeleteTurmaCommandHandler
    : IRequestHandler<DeleteTurmaCommand, Result<TurmaResponse, Error>>
{
    private readonly ITurmaRepository _turmaRepository;
    private readonly IMapper _mapper;

    public DeleteTurmaCommandHandler(
        ITurmaRepository turmaRepository,
        IMapper mapper)
    {
        _turmaRepository = turmaRepository;
        _mapper = mapper;
    }

    public async Task<Result<TurmaResponse, Error>> Handle(
        DeleteTurmaCommand request,
        CancellationToken cancellationToken)
    {
        var turma = await _turmaRepository.GetByIdAsync(request.Id);

        if (turma is null)
        {
            return ApplicationErrors.TurmaNotFound;
        }

        await _turmaRepository.DeleteAsync(turma);

        return _mapper.Map<TurmaResponse>(turma);
    }
}
