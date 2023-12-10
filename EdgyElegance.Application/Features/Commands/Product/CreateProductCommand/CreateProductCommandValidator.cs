using EdgyElegance.Application.Interfaces;
using FluentValidation;

namespace EdgyElegance.Application.Features.Commands.Product.CreateProductCommand;

internal class CreateProductCommandValidator  : AbstractValidator<CreateProductCommand> {
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandValidator(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;

        RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("{Property name} must not be null or empty");

        RuleFor(c => c)
            .MustAsync(NotExists);

        RuleFor(c => c)
            .MustAsync(HaveValidCategories);

        RuleFor(c => c)
            .MustAsync(HaveValidGenders);
    }

    private async Task<bool> HaveValidGenders(CreateProductCommand command, CancellationToken token) {
        var genders = await _unitOfWork.GenderRepository.GetManyAsync(x => command.Genders.Contains(x.Id));

        return genders.Count == command.Categories.Count;
    }

    private async Task<bool> HaveValidCategories(CreateProductCommand command, CancellationToken token) {
        var categories = await _unitOfWork.CategoryRepository.GetManyAsync(x => command.Categories.Contains(x.Id));

        return categories.Count == command.Categories.Count;
    }

    private async Task<bool> NotExists(CreateProductCommand command, CancellationToken token) {
        return await _unitOfWork.ProductRepository.ExistsAsync(command.Name) is false;
    }
}
