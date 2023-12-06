using MediatR;

namespace EdgyElegance.Application.Features.Commands.Product.DeleteProductCommand;

public record DeleteProductCommand(int Id) : IRequest<Unit>;
