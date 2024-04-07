using AutoMapper;
using Fiap.Api.Escola.Application.Contracts.Responses;
using Fiap.Api.Escola.Domain.Abstractions;
using MediatR;

namespace Fiap.Api.Escola.Application.Alunos.Queries.GetAll;

internal sealed class GetAlunosQueryHandler
    : IRequestHandler<GetAllAlunosQuery, IEnumerable<AlunoResponse>?>
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IMapper _mapper;

    public GetAlunosQueryHandler(
        IAlunoRepository alunoRepository,
        IMapper mapper)
    {
        _alunoRepository = alunoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AlunoResponse>?> Handle(
        GetAllAlunosQuery request,
        CancellationToken cancellationToken)
    {
        var alunos = await _alunoRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<AlunoResponse>>(alunos);
    }
}
