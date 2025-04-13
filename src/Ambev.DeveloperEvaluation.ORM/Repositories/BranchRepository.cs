using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Repository implementation for managing Branch entities in the database.
/// Provides methods to retrieve and manipulate Branch data.
/// </summary>
public class BranchRepository : IBranchRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="BranchRepository"/> class.
    /// </summary>
    /// <param name="context">The database context used for data access.</param>
    public BranchRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves a Branch entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Branch.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the Branch entity
    /// if found; otherwise, null.
    /// </returns>
    public async Task<Branch?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var branch = await _context.Branches
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        return branch;
    }
}
