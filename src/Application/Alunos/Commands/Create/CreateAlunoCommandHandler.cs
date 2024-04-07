using Fiap.Api.Escola.Domain.Abstractions;
using Fiap.Api.Escola.Domain.Entities;
using Fiap.Api.Escola.Domain.Shared;
using FluentValidation;
using MediatR;

namespace Fiap.Api.Escola.Application.Alunos.Commands.Create;

internal sealed class CreateAlunoCommandHandler
    : IRequestHandler<CreateAlunoCommand, Result<bool, Error>>
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IValidator<CreateAlunoCommand> _validator;

    public CreateAlunoCommandHandler(
        IAlunoRepository alunoRepository,
        IValidator<CreateAlunoCommand> validator)
    {
        _alunoRepository = alunoRepository;
        _validator = validator;
    }

    public async Task<Result<bool, Error>> Handle(
        CreateAlunoCommand request,
        CancellationToken cancellationToken)
    {
        var validation = _validator.Validate(request);

        if (!validation.IsValid)
        {
            return new Error(
                "CreateAluno.Validation",
                validation.Errors.ToString());
        }

        var criarAlunoResult = Aluno.Create(
            request.Nome,
            request.Usuario,
            request.Senha);

        if (criarAlunoResult.IsFailure)
        {
            return criarAlunoResult.Error!;
        }

        return await _alunoRepository.AddAsync(criarAlunoResult.Value!);
    }
}
