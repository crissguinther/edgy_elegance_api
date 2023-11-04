using AutoMapper;
using EdgyElegance.Application.Models;
using EdgyElegance.Identity.Entities;

namespace EdgyElegance.Application.Mappings {
    public class UserMapping : Profile {
        public UserMapping() {
            CreateMap<UserModel, ApplicationUser>()
                .ForSourceMember(um => um.Password, opt => opt.DoNotValidate())
                .ForSourceMember(um => um.PasswordConfirmation, opt => opt.DoNotValidate());
        }
    }
}
