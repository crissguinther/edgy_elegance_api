using EdgyElegance.Application.Exception;
using EdgyElegance.Application.Interfaces;
using MediatR;

namespace EdgyElegance.Application.Features.Commands.Product.DeleteProductCommand;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit> {
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken) {
        var product = await _unitOfWork.ProductRepository.FindByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Domain.Entities.Product), request.Id);

        _unitOfWork.ProductRepository.Delete(product);
        _unitOfWork.Commit();

        return Unit.Value;
    }
}
