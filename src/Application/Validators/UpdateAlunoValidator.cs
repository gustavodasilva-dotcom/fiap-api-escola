using Fiap.Api.Escola.Application.Alunos.Commands.Update;
using FluentValidation;

namespace Fiap.Api.Escola.Application.Validators;

public class UpdateAlunoValidator : AbstractValidator<UpdateAlunoCommand>
{
    public UpdateAlunoValidator()
    {
        RuleFor(command => command.Nome)
            .NotEmpty()
            .NotNull()
                .WithMessage("Nome do usuário não pode estar vazio ou nulo");

        RuleFor(command => command.Usuario)
            .NotEmpty()
            .NotNull()
                .WithMessage("Nome de usuário do usuário não pode estar vazio ou nulo");

        RuleFor(command => command.Senha)
            .NotEmpty()
            .NotNull()
                .WithMessage("Senha do usuário não pode ser vazia ou nula");
    }
}
