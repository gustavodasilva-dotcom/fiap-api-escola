using Fiap.Api.Escola.Domain.Abstractions;
using Fiap.Api.Escola.Domain.Entities;
using Fiap.Api.Escola.Domain.Shared;
using MediatR;

namespace Fiap.Api.Escola.Application.Turmas.Commands.Create;

internal sealed class CreateTurmaCommandHandler
    : IRequestHandler<CreateTurmaCommand, Result<bool, Error>>
{
    private readonly ITurmaRepository _turmaRepository;

    public CreateTurmaCommandHandler(ITurmaRepository turmaRepository)
    {
        _turmaRepository = turmaRepository;
    }

    public async Task<Result<bool, Error>> Handle(
        CreateTurmaCommand request,
        CancellationToken cancellationToken)
    {
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
