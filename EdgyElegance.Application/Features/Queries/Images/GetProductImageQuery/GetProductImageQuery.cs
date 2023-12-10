using MediatR;

namespace EdgyElegance.Application.Features.Queries.Images.GetProductImageQuery;

public record GetProductImageQuery(int Id) : IRequest<Stream>;
