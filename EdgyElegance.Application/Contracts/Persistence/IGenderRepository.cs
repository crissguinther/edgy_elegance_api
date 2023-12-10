using EdgyElegance.Domain.Entities;
using System.Linq.Expressions;

namespace EdgyElegance.Application.Contracts.Persistence;
public interface IGenderRepository {
    /// <summary>
    /// Adds a new <see cref="Gender"/> to the database
    /// </summary>
    /// <param name="gender">The <see cref="Gender"/> to be added</param>
    /// <returns>The added entry</returns>
    Task<Gender> AddAsync(Gender gender);

    /// <summary>
    /// Gets a <see cref="Gender"/> through its ID
    /// </summary>
    /// <param name="id">The ID to search for</param>
    /// <returns>The <see cref="Gender"/> if exists, null otherwise</returns>
    Task<Gender?> FindByIdAsync(int id);

    /// <summary>
    /// Updates a <see cref="Gender"/> entry
    /// </summary>
    /// <param name="gender">The <see cref="Gender"/> to be updated</param>
    void Update(Gender gender);

    /// <summary>
    /// Deletes a <see cref="Gender"/> from the database
    /// </summary>
    /// <param name="gender">The <see cref="Gender"/> to be deleted</param>
    void Delete(Gender gender);

    /// <summary>
    /// Checks if a <see cref="Gender"/> exists through its name
    /// </summary>
    /// <param name="genderName">The <see cref="Gender"/>'s name</param>
    /// <returns><see cref="true"/> if exists, <see cref="false"/> otherwise</returns>
    Task<bool> ExistsAsync(string genderName);

    Task<List<Gender>> GetManyAsync(Expression<Func<Gender, bool>> predicate);
}
