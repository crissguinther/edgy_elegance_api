using AutoMapper;
using EdgyElegance.Application.Exception;
using EdgyElegance.Application.Interfaces;
using MediatR;

namespace EdgyElegance.Application.Features.Queries.Product.GetProductDetailsQuery;

public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ProductDetailsDto> {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductDetailsDto> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken) {
        var product = await _unitOfWork.ProductRepository.FindByIdAsync(
            request.Id,
            x => x.Categories,
            x => x.Genders
        ) ?? throw new NotFoundException(nameof(Domain.Entities.Product), request.Id);

        ProductDetailsDto dto = _mapper.Map<ProductDetailsDto>(product);

        return dto;
    }
}
