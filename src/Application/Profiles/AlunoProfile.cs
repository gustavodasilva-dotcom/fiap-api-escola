using AutoMapper;
using Fiap.Api.Escola.Application.Contracts.Responses;
using Fiap.Api.Escola.Domain.Entities;

namespace Fiap.Api.Escola.Application.Profiles;

internal class AlunoProfile : Profile
{
    public AlunoProfile()
    {
        CreateMap<Aluno, AlunoResponse>()
            .ForMember(
                dest => dest.Id,
                src => src.MapFrom(x => x.Id))
            .ForMember(
                dest => dest.Nome,
                src => src.MapFrom(x => x.Nome))
            .ForMember(
                dest => dest.Usuario,
                src => src.MapFrom(x => x.Usuario))
             .ForMember(
                dest => dest.Senha,
                src => src.MapFrom(x => x.Senha));
    }
}
