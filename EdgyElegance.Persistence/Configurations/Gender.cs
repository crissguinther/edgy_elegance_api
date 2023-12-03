using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EdgyElegance.Persistence.Configurations;
public class Gender : IEntityTypeConfiguration<Domain.Entities.Gender> {
    public void Configure(EntityTypeBuilder<Domain.Entities.Gender> builder) {
        builder.HasIndex(b => b.Name)
            .IsUnique();
    }
}
