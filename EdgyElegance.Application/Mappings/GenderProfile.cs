using AutoMapper;
using EdgyElegance.Application.Features.Commands.Gender.Commands.CreateGenderCommand;
using EdgyElegance.Application.Features.Commands.Gender.Commands.UpdateGenderCommand;
using EdgyElegance.Application.Features.Queries.Gender.GetGenderDetailsQuery;
using EdgyElegance.Domain.Entities;

namespace EdgyElegance.Application.Mappings;

internal class GenderProfile : Profile {
    public GenderProfile() {
        CreateMap<CreateGenderCommand, Gender>();

        CreateMap<UpdateGenderCommand, Gender>();

        CreateMap<Gender, GenderDetailsDto>();

        CreateMap<Gender, GenderDto>();
    }
}
