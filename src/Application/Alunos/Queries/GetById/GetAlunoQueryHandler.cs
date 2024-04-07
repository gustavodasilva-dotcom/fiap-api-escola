using AutoMapper;
using Fiap.Api.Escola.Application.Contracts.Responses;
using Fiap.Api.Escola.Application.Errors;
using Fiap.Api.Escola.Domain.Abstractions;
using Fiap.Api.Escola.Domain.Extensions;
using Fiap.Api.Escola.Domain.Shared;
using MediatR;

namespace Fiap.Api.Escola.Application.Alunos.Queries.GetById;

internal sealed class GetAlunoQueryHandler
    : IRequestHandler<GetAlunoByIdQuery, Result<AlunoResponse, Error>>
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IMapper _mapper;

    public GetAlunoQueryHandler(
        IAlunoRepository alunoRepository,
        IMapper mapper)
    {
        _alunoRepository = alunoRepository;
        _mapper = mapper;
    }

    public async Task<Result<AlunoResponse, Error>> Handle(
        GetAlunoByIdQuery request,
        CancellationToken cancellationToken)
    {
        var aluno = await _alunoRepository.GetByIdAsync(request.Id);

        if (aluno is null)
        {
            return ApplicationErrors.AlunoNotFound;
        }

        aluno.Senha = aluno.Senha.DecryptString();

        return _mapper.Map<AlunoResponse>(aluno);
    }
}
