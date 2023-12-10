using MediatR;
using Microsoft.AspNetCore.Http;

namespace EdgyElegance.Application.Features.Commands.Image.UpdateProductImagesCommand;

public class UpdateProductImagesCommand : IRequest<Unit> {
    public int ProductId { get; set; }
    public List<IFormFile> Images { get; set; } = new();
}
