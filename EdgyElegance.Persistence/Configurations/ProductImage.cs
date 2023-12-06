using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EdgyElegance.Persistence.Configurations;

internal class ProductImage : IEntityTypeConfiguration<Domain.Entities.ProductImage> {
    public void Configure(EntityTypeBuilder<Domain.Entities.ProductImage> builder) {
        builder.HasOne(b => b.Product)
            .WithMany(b => b.Images)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.Thumbnail)
            .WithOne(b => b.ProductImage)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
