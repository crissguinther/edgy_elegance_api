using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EdgyElegance.Persistence.Configurations;

public class Category : IEntityTypeConfiguration<Domain.Entities.Category> {
    public void Configure(EntityTypeBuilder<Domain.Entities.Category> builder) {
        builder.HasIndex(c => c.Name)
            .IsUnique();
    }
}
