using EdgyElegance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EdgyElegance.Persistence.DatabaseContexts;

/// <summary>
/// The main context of the application
/// </summary>
public class ApplicationContext : DbContext {
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Gender> Genders { get; set; }
    public virtual DbSet<Product> Products { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges() {
        SetModifiedOn();

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default) {
        SetModifiedOn();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    /// <summary>
    /// Sets the <see cref="BaseEntity.ModifiedOn"/> values for the modified entries
    /// of the <see cref="DbContext.ChangeTracker"/>
    /// </summary>
    private void SetModifiedOn() {
        ChangeTracker.Entries().ToList().ForEach(entry => {
            PropertyInfo? modifiedOn = entry.Entity.GetType().GetProperty(nameof(BaseEntity.ModifiedOn));
            if (modifiedOn is not null && entry.State == EntityState.Modified)
                modifiedOn.SetValue(entry.Entity, DateTime.UtcNow);
        });
    }
}
