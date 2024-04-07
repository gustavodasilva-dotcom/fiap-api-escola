using AutoMapper;
using Fiap.Api.Escola.Application.Contracts.Responses;
using Fiap.Api.Escola.Application.Errors;
using Fiap.Api.Escola.Domain.Abstractions;
using Fiap.Api.Escola.Domain.Entities;
using Fiap.Api.Escola.Domain.Shared;
using FluentValidation;
using MediatR;

namespace Fiap.Api.Escola.Application.Turmas.Commands.Update;

internal sealed class UpdateTurmaCommandHandler
    : IRequestHandler<UpdateTurmaCommand, Result<TurmaResponse, Error>>
{
    private readonly ITurmaRepository _turmaRepository;
    private readonly IValidator<UpdateTurmaCommand> _validator;
    private readonly IMapper _mapper;

    public UpdateTurmaCommandHandler(
        ITurmaRepository turmaRepository,
        IValidator<UpdateTurmaCommand> validator,
        IMapper mapper)
    {
        _turmaRepository = turmaRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<Result<TurmaResponse, Error>> Handle(
        UpdateTurmaCommand request,
        CancellationToken cancellationToken)
    {
        var validation = _validator.Validate(request);

        if (!validation.IsValid)
        {
            return new Error(
                "UpdateTurma.Validation",
                validation.Errors.ToString());
        }

        var turma = await _turmaRepository.GetByIdAsync(request.Id);

        if (turma is null)
        {
            return ApplicationErrors.TurmaNotFound;
        }

        var turmaMesmoNome = await _turmaRepository
            .GetTurmaMesmoNomeAsync(request.Turma.Trim().ToLower());

        if (turmaMesmoNome is not null && turmaMesmoNome.Id != request.Id)
        {
            return ApplicationErrors.TurmaMesmoNomeExistente;
        }

        var atualizarTurmaResult = Turma.Update(
            request.Id,
            request.CursoId,
            request.Turma,
            request.Ano);

        if (atualizarTurmaResult.IsFailure)
        {
            return atualizarTurmaResult.Error!;
        }

        await _turmaRepository.UpdateAsync(atualizarTurmaResult.Value!);

        return _mapper.Map<TurmaResponse>(atualizarTurmaResult.Value!);
    }
}
