using FluentValidation;

namespace EdgyElegance.Application.Features.Commands.Image.UpdateProductImagesCommand;

internal class UpdateProductImagesCommandHandlerValidator : AbstractValidator<UpdateProductImagesCommand> {
    public UpdateProductImagesCommandHandlerValidator() {
        RuleFor(c => c)
            .Must(ContainsOnlyValidImages);
    }

    private bool ContainsOnlyValidImages(UpdateProductImagesCommand handler) {
        return handler.Images.Any(i => i.ContentType.Split("/")[0].Trim() != "image") == false;
    }
}
