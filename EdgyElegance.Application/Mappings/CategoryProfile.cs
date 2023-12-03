using AutoMapper;
using EdgyElegance.Application.Features.Commands.Category.CreateCategoryCommand;
using EdgyElegance.Application.Features.Commands.Category.UpdateCategoryCommand;
using EdgyElegance.Application.Features.Queries.Category;
using EdgyElegance.Domain.Entities;

namespace EdgyElegance.Application.Mappings {
    internal class CategoryProfile : Profile {
        public CategoryProfile() {
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<UpdateCategoryCommand, Category>();
            CreateMap<Category, CategoryDetailsDTO>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
