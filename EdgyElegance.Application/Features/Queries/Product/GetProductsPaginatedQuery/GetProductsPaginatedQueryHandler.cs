using AutoMapper;
using EdgyElegance.Application.Interfaces;
using EdgyElegance.Domain.Entities;
using MediatR;

namespace EdgyElegance.Application.Features.Queries.Product.GetProductsPaginatedQuery;

public class GetProductsPaginatedQueryHandler : IRequestHandler<GetProductsPaginatedQuery, List<ProductDto>> {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductsPaginatedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> Handle(GetProductsPaginatedQuery request, CancellationToken cancellationToken) {
        List<Domain.Entities.Product> products = await _unitOfWork.ProductRepository
            .GetProductsPaginated(request.Page, request.Size, request,
                x => x.Images, x => (x.Images as ProductImage)!.Thumbnail!, x => x.Genders, x => x.Categories);

        var dtos = _mapper.Map<List<ProductDto>> (products);

        return dtos;
    }
}
