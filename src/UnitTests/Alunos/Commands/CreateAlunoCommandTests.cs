using Fiap.Api.Escola.Application.Alunos.Commands.Create;
using Fiap.Api.Escola.Domain.Abstractions;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Fiap.Api.Escola.UnitTests.Alunos.Commands;

public class CreateAlunoCommandTests
{
    private readonly Mock<IAlunoRepository> _alunoRepositoryMock;
    private readonly Mock<IValidator<CreateAlunoCommand>> _validatorMock;

    public CreateAlunoCommandTests()
    {
        _alunoRepositoryMock = new();
        _validatorMock = new();
    }

    [Fact(DisplayName = "Handle: deve retornar Result igual a Failure quando a senha for pequena")]
    public async Task Handle_Deve_RetornarResultFailure_QuandoSenhaForPequena()
    {
        // Arrange
        var command = new CreateAlunoCommand(
            Nome: "Lewis Hamilton",
            Usuario: "lewis.hamilton",
            Senha: "Lewis1!");

        var handler = new CreateAlunoCommandHandler(
            _alunoRepositoryMock.Object,
            _validatorMock.Object);

        _validatorMock.Setup(validator =>
            validator.Validate(It.IsAny<CreateAlunoCommand>()))
                .Returns(new ValidationResult());

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
    }
}