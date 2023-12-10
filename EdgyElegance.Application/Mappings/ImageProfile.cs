using AutoMapper;
using EdgyElegance.Application.Features.Queries.Product.GetProductsPaginatedQuery;
using EdgyElegance.Domain.Entities;

namespace EdgyElegance.Application.Mappings;

public class ImageProfile : Profile{
    public ImageProfile() {
        CreateMap<ProductImage, ProductImageDto>()
            .ForMember(dst => dst.ThumbnailId, opt => opt.MapFrom(x => x.Thumbnail.Id));
    }
}
