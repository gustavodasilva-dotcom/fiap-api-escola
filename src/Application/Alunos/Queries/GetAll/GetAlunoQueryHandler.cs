using Fiap.Api.Escola.Domain.Abstractions;
using Fiap.Api.Escola.Domain.Entities;
using MediatR;

namespace Fiap.Api.Escola.Application.Alunos.Queries.GetAll;

internal sealed class GetAlunoQueryHandler
    : IRequestHandler<GetAllAlunosQuery, IEnumerable<Aluno>?>
{
    private readonly IAlunoRepository _alunoRepository;

    public GetAlunoQueryHandler(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    public async Task<IEnumerable<Aluno>?> Handle(
        GetAllAlunosQuery request,
        CancellationToken cancellationToken)
    {
        return await _alunoRepository.GetAllAsync();;
    }
}
