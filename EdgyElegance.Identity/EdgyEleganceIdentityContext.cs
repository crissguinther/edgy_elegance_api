using EdgyElegance.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EdgyElegance.Identity {
    public class EdgyEleganceIdentityContext : IdentityDbContext {
        public EdgyEleganceIdentityContext(DbContextOptions<EdgyEleganceIdentityContext> options) : base(options) { }

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<RefreshToken>()
                .HasOne(x => x.User)
                .WithMany(x => x.RefreshTokens)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
