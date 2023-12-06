using MediatR;

namespace EdgyElegance.Application.Features.Commands.Product.UpdateProductCommand;

public class UpdateProductCommand : IRequest<Unit> {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public List<int> Categories = new();
    public List<int> Genders = new();
}
