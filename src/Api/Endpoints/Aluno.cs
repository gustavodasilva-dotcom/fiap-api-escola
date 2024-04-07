using Carter;
using Fiap.Api.Escola.Application.Alunos.Commands.Create;
using Fiap.Api.Escola.Application.Alunos.Commands.Delete;
using Fiap.Api.Escola.Application.Alunos.Commands.Update;
using Fiap.Api.Escola.Application.Alunos.Queries.GetAll;
using Fiap.Api.Escola.Application.Alunos.Queries.GetById;
using Fiap.Api.Escola.Application.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Escola.Api.Endpoints;

public class Aluno : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("alunos", async (ISender sender) =>
        {
            var alunos = await sender.Send(new GetAllAlunosQuery());

            if (alunos is null)
            {
                return Results.NoContent();
            }

            return Results.Ok(alunos);
        });

        app.MapGet("alunos/{id:int}", async (int id, ISender sender) =>
        {
            var alunoResult = await sender.Send(new GetAlunoByIdQuery(id));

            if (alunoResult.IsFailure)
            {
                return Results.NotFound(alunoResult.Value);
            }

            return Results.Ok(alunoResult.Value);
        });

        app.MapPost("alunos", async (CreateAlunoCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error!.Message);
            }

            return Results.Created();
        });

        app.MapPut("alunos/{id:int}", async (int id, [FromBody] UpdateAlunoRequest request, ISender sender) =>
        {
            var command = new UpdateAlunoCommand(
                id,
                request.Nome,
                request.Usuario,
                request.Senha);

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error!.Message);
            }

            return Results.Ok(result.Value);
        });

        app.MapDelete("alunos/{id:int}", async (int id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteAlunoCommand(id));

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error!.Message);
            }

            return Results.NoContent();
        });
    }
}
