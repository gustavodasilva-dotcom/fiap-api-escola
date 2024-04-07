using Carter;
using Fiap.Api.Escola.Application.Contracts.Requests;
using Fiap.Api.Escola.Application.Turmas.Commands.Create;
using Fiap.Api.Escola.Application.Turmas.Commands.Delete;
using Fiap.Api.Escola.Application.Turmas.Commands.Update;
using Fiap.Api.Escola.Application.Turmas.Queries.GetAll;
using Fiap.Api.Escola.Application.Turmas.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Escola.Api.Endpoints;

public class Turma : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("turmas", async (ISender sender) =>
        {
            var turmas = await sender.Send(new GetAllTurmasQuery());

            if (turmas is null)
            {
                return Results.NoContent();
            }

            return Results.Ok(turmas);
        });

        app.MapGet("turmas/{id:int}", async (int id, ISender sender) =>
        {
            var turmaResult = await sender.Send(new GetTurmaByIdQuery(id));

            if (turmaResult.IsFailure)
            {
                return Results.NotFound(turmaResult.Value);
            }

            return Results.Ok(turmaResult.Value);
        });

        app.MapPost("turmas", async (CreateTurmaCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error!.Message);
            }

            return Results.Created();
        });

        app.MapPut("turmas/{id:int}", async (int id, [FromBody] UpdateTurmaRequest request, ISender sender) =>
        {
            var command = new UpdateTurmaCommand(
                id,
                request.CursoId,
                request.Turma,
                request.Ano);

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error!.Message);
            }

            return Results.Ok(result.Value);
        });

        app.MapDelete("turmas/{id:int}", async (int id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteTurmaCommand(id));

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error!.Message);
            }

            return Results.NoContent();
        });
    }
}
