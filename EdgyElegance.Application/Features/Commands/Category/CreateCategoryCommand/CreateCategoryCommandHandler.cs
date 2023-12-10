using AutoMapper;
using EdgyElegance.Application.Exception;
using EdgyElegance.Application.Interfaces;
using MediatR;

namespace EdgyElegance.Application.Features.Commands.Category.CreateCategoryCommand;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int> {
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateCategoryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken) {
        var validator = new CategoryCommandValidator(_unitOfWork);
        var validation = await validator.ValidateAsync(request);

        if (!validation.IsValid)
            throw new BadRequestException(validation);

        Domain.Entities.Category category = _mapper.Map<Domain.Entities.Category>(request);

        var result = await _unitOfWork.CategoryRepository.AddAsync(category);
        _unitOfWork.Commit();

        return result.Id;
    }
}
