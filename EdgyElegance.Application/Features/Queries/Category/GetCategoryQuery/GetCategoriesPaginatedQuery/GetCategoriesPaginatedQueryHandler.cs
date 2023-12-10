using AutoMapper;
using EdgyElegance.Application.Interfaces;
using MediatR;

namespace EdgyElegance.Application.Features.Queries.Category.GetCategoryQuery.GetCategoriesPaginatedQuery;
public class GetCategoriesPaginatedQueryHandler : IRequestHandler<GetCategoriesPaginatedQuery, List<CategoryDto>> {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoriesPaginatedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<List<CategoryDto>> Handle(GetCategoriesPaginatedQuery request, CancellationToken cancellationToken) {
        var categories = _unitOfWork.CategoryRepository.GetPaginated(request.Page, request.Count, request);
        List<CategoryDto> categoryDtos = _mapper.Map<List<Domain.Entities.Category>, List<CategoryDto>>(categories);
        return Task.FromResult(categoryDtos);
    }
}
