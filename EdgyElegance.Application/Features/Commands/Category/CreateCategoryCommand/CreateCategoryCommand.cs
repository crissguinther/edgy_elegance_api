using MediatR;

namespace EdgyElegance.Application.Features.Commands.Category.CreateCategoryCommand;

public class CreateCategoryCommand : IRequest<int> {
    public string Name { get; set; } = string.Empty;
}
