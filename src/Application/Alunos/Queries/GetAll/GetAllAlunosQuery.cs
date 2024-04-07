using Fiap.Api.Escola.Application.Contracts.Responses;
using MediatR;

namespace Fiap.Api.Escola.Application.Alunos.Queries.GetAll;

public record GetAllAlunosQuery() : IRequest<IEnumerable<AlunoResponse>?>;
