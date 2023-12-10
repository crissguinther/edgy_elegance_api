using EdgyElegance.Application.Exception;
using EdgyElegance.Application.Interfaces;
using MediatR;

namespace EdgyElegance.Application.Features.Commands.Gender.Commands.DeleteGenderCommand;

public class DeleteGenderCommandHandler : IRequestHandler<DeleteGenderCommand, Unit> {
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGenderCommandHandler(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteGenderCommand request, CancellationToken cancellationToken) {
        Domain.Entities.Gender gender = await _unitOfWork.GenderRepository.FindByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Domain.Entities.Gender), request.Id);

        _unitOfWork.GenderRepository.Delete(gender);
        _unitOfWork.Commit();

        return Unit.Value;
    }
}
