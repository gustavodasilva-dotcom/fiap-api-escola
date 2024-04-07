using Fiap.Api.Escola.Application.Turmas.Commands.Update;
using FluentValidation;

namespace Fiap.Api.Escola.Application.Validators;

public class UpdateTurmaValidator : AbstractValidator<UpdateTurmaCommand>
{
    public UpdateTurmaValidator()
    {
        RuleFor(command => command.Turma)
            .NotEmpty()
            .NotNull()
                .WithMessage("Nome da turma não pode ser vazio ou nulo");
    }
}
