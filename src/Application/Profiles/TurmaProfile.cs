using AutoMapper;
using Fiap.Api.Escola.Application.Contracts.Responses;
using Fiap.Api.Escola.Domain.Entities;

namespace Fiap.Api.Escola.Application.Profiles;

internal class TurmaProfile : Profile
{
    public TurmaProfile()
    {
        CreateMap<Turma, TurmaResponse>()
            .ForMember(
                dest => dest.Id,
                src => src.MapFrom(x => x.Id))
            .ForMember(
                dest => dest.CursoID,
                src => src.MapFrom(x => x.CursoID))
            .ForMember(
                dest => dest.Nome,
                src => src.MapFrom(x => x.Nome))
            .ForMember(
                dest => dest.Ano,
                src => src.MapFrom(x => x.Ano));
    }
}
