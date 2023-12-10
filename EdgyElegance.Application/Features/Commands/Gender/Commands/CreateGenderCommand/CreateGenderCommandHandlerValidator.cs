using EdgyElegance.Application.Interfaces;
using FluentValidation;

namespace EdgyElegance.Application.Features.Commands.Gender.Commands.CreateGenderCommand;

public class CreateGenderCommandHandlerValidator : AbstractValidator<CreateGenderCommand> {
    private IUnitOfWork _unitOfWork;

    public CreateGenderCommandHandlerValidator(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;

        RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(1)
            .WithMessage("{Property name} must be at least 1 characters long");

        RuleFor(c => c)
            .MustAsync(NotExists);
    }

    private async Task<bool> NotExists(CreateGenderCommand command, CancellationToken token) {
        return await _unitOfWork.GenderRepository.ExistsAsync(command.Name) == false;
    }
}
