using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EdgyElegance.Domain.Entities;

public class ProductImageThumbnail  : BaseImage {
    [Required]
    public ProductImage? ProductImage { get; set; }

    [ForeignKey(nameof(ProductImage))]
    public int ProductImageId { get; set; }
}
