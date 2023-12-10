using MediatR;

namespace EdgyElegance.Application.Features.Queries.Category.GetCategoryQuery.GetCategoryDetailsQuery;
public record GetCategoryDetailsQuery(int Id) : IRequest<CategoryDetailsDTO>;
