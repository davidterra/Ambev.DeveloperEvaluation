using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for User entity operations.
/// Provides methods for creating, retrieving, updating, deleting, and listing users.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Creates a new user in the repository.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>The created user.</returns>
    Task<User> CreateAsync(User user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>The user if found, null otherwise.</returns>
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    /// <param name="email">The email address to search for.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>The user if found, null otherwise.</returns>
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a user from the repository.
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>True if the user was deleted, false if not found.</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing user in the repository.
    /// </summary>
    /// <param name="user">The user with updated information.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>The updated user.</returns>
    Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists users with optional ordering and filtering.
    /// </summary>
    /// <param name="orderBy">The field to order the results by.</param>
    /// <param name="filters">Optional filters to apply to the user list.</param>
    /// <returns>An IQueryable of users matching the criteria.</returns>
    IQueryable<User> ListUsers(string orderBy, Dictionary<string, string>? filters);
}
