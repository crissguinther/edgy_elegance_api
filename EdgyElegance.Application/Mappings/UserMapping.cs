using AutoMapper;
using EdgyElegance.Application.Models;
using EdgyElegance.Application.Models.RequestModels;
using EdgyElegance.Identity.Entities;

namespace EdgyElegance.Application.Mappings {
    public class UserMapping : Profile {
        public UserMapping() {
            CreateMap<CreateUserRequest, ApplicationUser>()
                .ForSourceMember(um => um.Password, opt => opt.DoNotValidate())
                .ForSourceMember(um => um.PasswordConfirmation, opt => opt.DoNotValidate())
                .ForMember(x => x.UserName, u => u.MapFrom(s => s.Email));

            CreateMap<ApplicationUser, UserModel>()
                .ReverseMap();
        }
    }
}
