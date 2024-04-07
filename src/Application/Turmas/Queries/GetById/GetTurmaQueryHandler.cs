using AutoMapper;
using Fiap.Api.Escola.Application.Contracts.Responses;
using Fiap.Api.Escola.Application.Errors;
using Fiap.Api.Escola.Domain.Abstractions;
using Fiap.Api.Escola.Domain.Shared;
using MediatR;

namespace Fiap.Api.Escola.Application.Turmas.Queries.GetById;

internal sealed class GetTurmaQueryHandler
    : IRequestHandler<GetTurmaByIdQuery, Result<TurmaResponse, Error>>
{
    private readonly ITurmaRepository _turmaRepository;
    private readonly IMapper _mapper;

    public GetTurmaQueryHandler(
        ITurmaRepository turmaRepository,
        IMapper mapper)
    {
        _turmaRepository = turmaRepository;
        _mapper = mapper;
    }

    public async Task<Result<TurmaResponse, Error>> Handle(
        GetTurmaByIdQuery request,
        CancellationToken cancellationToken)
    {
        var aluno = await _turmaRepository.GetByIdAsync(request.Id);

        if (aluno is null)
        {
            return ApplicationErrors.TurmaNotFound;
        }

        return _mapper.Map<TurmaResponse>(aluno);
    }
}
