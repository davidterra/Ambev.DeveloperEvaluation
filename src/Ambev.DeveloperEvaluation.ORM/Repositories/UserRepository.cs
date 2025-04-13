using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Query;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IUserRepository using Entity Framework Core.
/// Provides methods for CRUD operations and user-specific queries.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository"/> class.
    /// </summary>
    /// <param name="context">The database context used for data access.</param>
    public UserRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new user in the database.
    /// </summary>
    /// <param name="user">The user entity to be created.</param>
    /// <param name="cancellationToken">Optional cancellation token for the operation.</param>
    /// <returns>The created <see cref="User"/> entity.</returns>
    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="cancellationToken">Optional cancellation token for the operation.</param>
    /// <returns>The <see cref="User"/> entity if found; otherwise, null.</returns>
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    /// <param name="email">The email address to search for.</param>
    /// <param name="cancellationToken">Optional cancellation token for the operation.</param>
    /// <returns>The <see cref="User"/> entity if found; otherwise, null.</returns>
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    /// <summary>
    /// Deletes a user from the database.
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete.</param>
    /// <param name="cancellationToken">Optional cancellation token for the operation.</param>
    /// <returns>True if the user was deleted; false if not found.</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FindAsync(id, cancellationToken);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Updates an existing user in the database.
    /// </summary>
    /// <param name="user">The user entity with updated information.</param>
    /// <param name="cancellationToken">Optional cancellation token for the operation.</param>
    /// <returns>The updated <see cref="User"/> entity.</returns>
    public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        user.UpdatedAt = DateTime.UtcNow;
        _context.Users.Attach(user);
        _context.Entry(user).State = EntityState.Modified;
        _context.Entry(user.Address).State = EntityState.Modified;
        _context.Entry(user.Address.GeoLocation).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    /// <summary>
    /// Retrieves a list of users with optional filtering and sorting.
    /// </summary>
    /// <param name="orderBy">The field to order the results by.</param>
    /// <param name="filters">Optional dictionary of filters to apply.</param>
    /// <returns>An <see cref="IQueryable{T}"/> of <see cref="User"/> entities.</returns>
    public IQueryable<User> ListUsers(string orderBy, Dictionary<string, string>? filters)
    {
        var users = _context.Users.AsQueryable();

        var query = new QueryFilterAndSorter<User>(users, orderBy, filters);
        return query.Apply();
    }
}
