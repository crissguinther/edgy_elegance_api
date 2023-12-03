using MediatR;

namespace EdgyElegance.Application.Features.Commands.Category.UpdateCategoryCommand;
public class UpdateCategoryCommand : IRequest<Unit> {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
