using EdgyElegance.Application.Features.Queries.Category;
using EdgyElegance.Application.Features.Queries.Gender.GetGenderDetailsQuery;

namespace EdgyElegance.Application.Features.Queries.Product.GetProductsPaginatedQuery;

public class ProductDto {
    public int Id { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<GenderDto>? Genders { get; set; }
    public ICollection<CategoryDto>? Categories { get; set; }
    public ICollection<ProductImageDto>? Images { get; set; }
}