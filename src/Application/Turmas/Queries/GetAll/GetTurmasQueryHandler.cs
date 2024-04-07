using AutoMapper;
using Fiap.Api.Escola.Application.Contracts.Responses;
using Fiap.Api.Escola.Domain.Abstractions;
using MediatR;

namespace Fiap.Api.Escola.Application.Turmas.Queries.GetAll;

internal sealed class GetTurmasQueryHandler
    : IRequestHandler<GetAllTurmasQuery, IEnumerable<TurmaResponse>?>
{
    private readonly ITurmaRepository _turmaRepository;
    private readonly IMapper _mapper;

    public GetTurmasQueryHandler(
        ITurmaRepository turmaRepository,
        IMapper mapper)
    {
        _turmaRepository = turmaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TurmaResponse>?> Handle(
        GetAllTurmasQuery request,
        CancellationToken cancellationToken)
    {
        var turmas = await _turmaRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<TurmaResponse>>(turmas);
    }
}
