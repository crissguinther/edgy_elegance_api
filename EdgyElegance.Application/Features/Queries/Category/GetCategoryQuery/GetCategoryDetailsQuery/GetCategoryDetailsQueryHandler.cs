using AutoMapper;
using EdgyElegance.Application.Exception;
using EdgyElegance.Application.Interfaces;
using MediatR;

namespace EdgyElegance.Application.Features.Queries.Category.GetCategoryQuery.GetCategoryDetailsQuery;
public class GetCategoryDetailsQueryHandler : IRequestHandler<GetCategoryDetailsQuery, CategoryDetailsDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoryDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CategoryDetailsDTO> Handle(GetCategoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.CategoryRepository.GetAsync(request.Id)
            ?? throw new NotFoundException(nameof(Domain.Entities.Category), request.Id);

        var categoryDto = _mapper.Map<CategoryDetailsDTO>(category!);
        return categoryDto;
    }
}
