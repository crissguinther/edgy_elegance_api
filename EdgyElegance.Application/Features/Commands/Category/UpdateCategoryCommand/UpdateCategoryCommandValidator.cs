using EdgyElegance.Application.Interfaces;
using FluentValidation;

namespace EdgyElegance.Application.Features.Commands.Category.UpdateCategoryCommand;
public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand> {
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandValidator(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("{Property name} must not be null or empty");

        RuleFor(x => x)
            .MustAsync(MustExistAsync);
    }

    private async Task<bool> MustExistAsync(UpdateCategoryCommand command, CancellationToken token) {
        return await _unitOfWork.CategoryRepository.GetAsync(command.Id) is not null;
    }
}
