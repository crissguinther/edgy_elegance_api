using AutoMapper;
using EdgyElegance.Application.Features.Commands.Product.CreateProductCommand;
using EdgyElegance.Application.Features.Commands.Product.UpdateProductCommand;
using EdgyElegance.Application.Features.Queries.Product.GetProductDetailsQuery;
using EdgyElegance.Application.Features.Queries.Product.GetProductsPaginatedQuery;
using EdgyElegance.Domain.Entities;

namespace EdgyElegance.Application.Mappings;

public class ProductProfile : Profile {
    public ProductProfile() {
        CreateMap<CreateProductCommand, Product>()
            .ForMember(x => x.Categories, opt => opt.Ignore())
            .ForMember(x => x.Genders, opt => opt.Ignore());

        CreateMap<UpdateProductCommand, Product>()
            .ForSourceMember(x => x.Categories, opt => opt.DoNotValidate())
            .ForSourceMember(x => x.Genders, opt => opt.DoNotValidate());

        CreateMap<Product, ProductDetailsDto>();

        CreateMap<Product, ProductDto>();
    }
}
