using Fiap.Api.Escola.Application.Turmas.Commands.Create;
using FluentValidation;

namespace Fiap.Api.Escola.Application.Validators;

public class CreateTurmaValidator : AbstractValidator<CreateTurmaCommand>
{
    public CreateTurmaValidator()
    {
        RuleFor(command => command.Turma)
            .NotEmpty()
            .NotNull()
                .WithMessage("Nome da turma não pode ser vazio ou nulo");
    }
}
