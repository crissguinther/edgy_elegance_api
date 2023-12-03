namespace EdgyElegance.Application.Features.Queries.Category;
public class CategoryDetailsDTO {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}
