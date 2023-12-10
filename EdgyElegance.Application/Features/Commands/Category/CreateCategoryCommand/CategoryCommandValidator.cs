using EdgyElegance.Application.Interfaces;
using FluentValidation;

namespace EdgyElegance.Application.Features.Commands.Category.CreateCategoryCommand;
public class CategoryCommandValidator : AbstractValidator<CreateCategoryCommand> {
    public IUnitOfWork _unitOfWork { get; set; }

    public CategoryCommandValidator(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;

        RuleFor(c => c.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("{Property name} must not be null or empty");

        RuleFor(c => c.Name)
            .MustAsync(MustNotExist)
            .WithMessage("{Property name} already exists on the database");
    }

    private async Task<bool> MustNotExist(string name, CancellationToken token) {
        return await _unitOfWork.CategoryRepository.CategoryExistsAsync(name) is false;
    }
}
