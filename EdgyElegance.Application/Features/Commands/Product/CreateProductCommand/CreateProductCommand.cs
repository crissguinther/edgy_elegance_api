using EdgyElegance.Domain.Enums;
using MediatR;

namespace EdgyElegance.Application.Features.Commands.Product.CreateProductCommand;

public class CreateProductCommand : IRequest<int> {
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public AgeGroup Age { get; set; }
    public Size Size { get; set; }
    public List<int> Categories { get; set; } = new();
    public List<int> Genders { get; set; } = new();
}
