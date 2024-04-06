using Fiap.Api.Escola.Domain.Abstractions;
using Fiap.Api.Escola.Domain.Entities;
using MediatR;

namespace Fiap.Api.Escola.Application.Alunos.Queries.GetById;

internal sealed class GetAlunoQueryHandler
    : IRequestHandler<GetByIdQuery, Aluno?>
{
    private readonly IAlunoRepository _alunoRepository;

    public GetAlunoQueryHandler(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    public async Task<Aluno?> Handle(
        GetByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _alunoRepository.GetByIdAsync(request.Id);
    }
}
