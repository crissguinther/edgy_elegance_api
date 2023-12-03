using System.ComponentModel.DataAnnotations.Schema;

namespace EdgyElegance.Domain.Entities;

/// <summary>
/// The base for the other entities of the application
/// </summary>
public abstract class BaseEntity {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// The <see cref="DateTime"/> where the entity was created
    /// </summary>
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The last <see cref="DateTime"/> where the entity was modified
    /// </summary>
    public DateTime? ModifiedOn { get; set; }
}
