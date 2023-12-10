using AutoMapper;
using EdgyElegance.Application.Exception;
using EdgyElegance.Application.Interfaces;
using MediatR;

namespace EdgyElegance.Application.Features.Commands.Category.UpdateCategoryCommand;
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit> {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken) {
        var validator = new UpdateCategoryCommandValidator(_unitOfWork);
        var validation = await validator.ValidateAsync(request, cancellationToken);
        var inDatabase = await _unitOfWork.CategoryRepository.GetAsync(request.Id);

        if (!validation.IsValid) {
            throw new BadRequestException(validation);
        }

        var category = _mapper.Map(request, inDatabase);
        _unitOfWork.CategoryRepository.Update(category!);
        _unitOfWork.Commit();

        return Unit.Value;
    }
}
