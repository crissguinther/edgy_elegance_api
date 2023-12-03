using EdgyElegance.Domain.Enums;

namespace EdgyElegance.Domain.Entities;

public class Product : BaseEntity {
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public AgeGroup Age { get; set; } = AgeGroup.Undefined;
    public Size Size { get; set; } = Size.S;
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    public virtual ICollection<Gender> Genders { get; set; } = new List<Gender>();
}
