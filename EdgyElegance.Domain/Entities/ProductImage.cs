using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EdgyElegance.Domain.Entities;

public class ProductImage : BaseImage {
    [Required]
    public Product? Product { get; set; }

    [ForeignKey(nameof(Product))]
    public int ProductId { get; set; }

    [Required]
    public ProductImageThumbnail? Thumbnail { get; set; }
}
