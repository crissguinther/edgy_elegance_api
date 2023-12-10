using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EdgyElegance.Persistence.Configurations;

public class Product : IEntityTypeConfiguration<Domain.Entities.Product> {
    public void Configure(EntityTypeBuilder<Domain.Entities.Product> builder) {
        builder.Property(b => b.Price)
            .HasColumnType("decimal(15,2)");

        builder.HasMany(b => b.Genders)
            .WithMany(b => b.Products);

        builder.HasMany(b => b.Categories)
            .WithMany(b => b.Products);

        builder.HasMany(b => b.Images)
            .WithOne(b => b.Product)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
