using EdgyElegance.Application.Exception;
using EdgyElegance.Application.Interfaces;
using MediatR;

namespace EdgyElegance.Application.Features.Commands.Category.DeleteCategoryCommand;
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit> {
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken) {
        var category = await _unitOfWork.CategoryRepository.GetAsync(request.Id) 
            ?? throw new NotFoundException(nameof(Domain.Entities.Category), request.Id);

        _unitOfWork.CategoryRepository.Delete(category);
        _unitOfWork.Commit();

        return Unit.Value;
    }
}
