using Carter;
using Fiap.Api.Escola.Application.Alunos.Commands.Create;
using Fiap.Api.Escola.Application.Alunos.Commands.Delete;
using Fiap.Api.Escola.Application.Alunos.Commands.Update;
using Fiap.Api.Escola.Application.Alunos.Queries.GetAll;
using Fiap.Api.Escola.Application.Alunos.Queries.GetById;
using MediatR;

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
            var aluno = await sender.Send(new GetByIdQuery(id));

            if (aluno is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(aluno);
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

        app.MapPut("alunos", async (UpdateAlunoCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error!.Message);
            }

            return Results.Ok(result.Value);
        });

        app.MapPatch("alunos/{id:int}", async (int id, ISender sender) =>
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
