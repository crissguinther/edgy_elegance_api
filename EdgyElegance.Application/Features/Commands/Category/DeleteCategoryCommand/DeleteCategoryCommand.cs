using MediatR;

namespace EdgyElegance.Application.Features.Commands.Category.DeleteCategoryCommand;

public record DeleteCategoryCommand(int Id) : IRequest<Unit>;
