using MediatR;

namespace EdgyElegance.Application.Features.Queries.Product.GetProductsPaginatedQuery;

public class GetProductsPaginatedQuery : IRequest<List<ProductDto>> {
    public int Page { get; set; }
    public int Size { get; set; }
    public string Name { get; set; } = string.Empty;
};
