using MediatR;

namespace EdgyElegance.Application.Features.Queries.Images.GetProductImageQuery;

public record GetProductThumbnailImageQuery(int Id) : IRequest<Stream>;
