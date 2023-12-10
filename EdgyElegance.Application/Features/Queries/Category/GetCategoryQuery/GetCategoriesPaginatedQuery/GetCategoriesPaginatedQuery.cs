using MediatR;

namespace EdgyElegance.Application.Features.Queries.Category.GetCategoryQuery.GetCategoriesPaginatedQuery;
public class GetCategoriesPaginatedQuery : IRequest<List<CategoryDto>> {
    public int Page { get; set; }
    public int Count { get; set; }
    public string Name { get; set; } = string.Empty;
}
