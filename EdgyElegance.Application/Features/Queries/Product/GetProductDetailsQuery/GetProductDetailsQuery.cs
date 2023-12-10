using MediatR;

namespace EdgyElegance.Application.Features.Queries.Product.GetProductDetailsQuery;

public record GetProductDetailsQuery(int Id) : IRequest<ProductDetailsDto>;