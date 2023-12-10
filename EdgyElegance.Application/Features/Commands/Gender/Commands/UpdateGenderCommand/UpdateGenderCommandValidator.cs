using EdgyElegance.Application.Interfaces;
using FluentValidation;

namespace EdgyElegance.Application.Features.Commands.Gender.Commands.UpdateGenderCommand;

internal class UpdateGenderCommandValidator : AbstractValidator<UpdateGenderCommand> {
    private readonly IUnitOfWork _unitOfWork;

    public UpdateGenderCommandValidator(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;

        RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("{Property name} must not be null or empty");

        RuleFor(c => c)
            .MustAsync(NotExists);
    }

    private async Task<bool> NotExists(UpdateGenderCommand command, CancellationToken token) {
        return await _unitOfWork.GenderRepository.ExistsAsync(command.Name) is false;
    }
}
