using EdgyElegance.Application.Features.Queries.Category;
using EdgyElegance.Application.Features.Queries.Gender.GetGenderDetailsQuery;

namespace EdgyElegance.Application.Features.Queries.Product.GetProductDetailsQuery;

public class ProductDetailsDto {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ICollection<CategoryDto>? Categories { get; set; }
    public ICollection<GenderDto>? Genders { get; set; }
}