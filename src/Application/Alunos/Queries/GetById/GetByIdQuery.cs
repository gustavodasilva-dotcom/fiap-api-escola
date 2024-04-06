using Fiap.Api.Escola.Domain.Entities;
using MediatR;

namespace Fiap.Api.Escola.Application.Alunos.Queries.GetById;

public record GetByIdQuery(int Id) : IRequest<Aluno?>;
