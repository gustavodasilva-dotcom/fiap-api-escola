using Fiap.Api.Escola.Application.Errors;
using Fiap.Api.Escola.Domain.Abstractions;
using Fiap.Api.Escola.Domain.Entities;
using Fiap.Api.Escola.Domain.Shared;
using FluentValidation;
using MediatR;

namespace Fiap.Api.Escola.Application.Turmas.Commands.Create;

internal sealed class CreateTurmaCommandHandler
    : IRequestHandler<CreateTurmaCommand, Result<bool, Error>>
{
    private readonly ITurmaRepository _turmaRepository;
    private readonly IValidator<CreateTurmaCommand> _validator;

    public CreateTurmaCommandHandler(
        ITurmaRepository turmaRepository,
        IValidator<CreateTurmaCommand> validator)
    {
        _turmaRepository = turmaRepository;
        _validator = validator;
    }

    public async Task<Result<bool, Error>> Handle(
        CreateTurmaCommand request,
        CancellationToken cancellationToken)
    {
        var validation = _validator.Validate(request);

        if (!validation.IsValid)
        {
            return new Error(
                "CreateTurma.Validation",
                validation.Errors.ToString());
        }

        var turmaMesmoNome = await _turmaRepository
            .GetTurmaMesmoNomeAsync(request.Turma.Trim().ToLower());

        if (turmaMesmoNome is not null)
        {
            return ApplicationErrors.TurmaMesmoNomeExistente;
        }

        var criarTurmaResult = Turma.Create(
            request.CursoId,
            request.Turma,
            request.Ano);

        if (criarTurmaResult.IsFailure)
        {
            return criarTurmaResult.Error!;
        }

        return await _turmaRepository.AddAsync(criarTurmaResult.Value!);
    }
}
