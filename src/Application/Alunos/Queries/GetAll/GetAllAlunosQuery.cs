using Fiap.Api.Escola.Domain.Entities;
using MediatR;

namespace Fiap.Api.Escola.Application.Alunos.Queries.GetAll;

public record GetAllAlunosQuery() : IRequest<IEnumerable<Aluno>?>;
