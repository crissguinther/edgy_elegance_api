using FluentValidation;

namespace EdgyElegance.Application.Features.Commands.Product.UpdateProductCommand;

public class UpdateProductCommandHandlerValidator : AbstractValidator<UpdateProductCommand> {
    public UpdateProductCommandHandlerValidator() {
        RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("{Property name} must not be null or empty");
    }
}