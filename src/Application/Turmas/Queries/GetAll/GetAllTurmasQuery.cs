using Fiap.Api.Escola.Application.Contracts.Responses;
using MediatR;

namespace Fiap.Api.Escola.Application.Turmas.Queries.GetAll;

public record GetAllTurmasQuery() : IRequest<IEnumerable<TurmaResponse>?>;
