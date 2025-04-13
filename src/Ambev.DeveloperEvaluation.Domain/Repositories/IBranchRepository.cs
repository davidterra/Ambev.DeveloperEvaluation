using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Represents a repository interface for managing Branch entities.
    /// Provides methods to retrieve Branch data from the data source.
    /// </summary>
    public interface IBranchRepository
    {
        /// <summary>
        /// Retrieves a Branch entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Branch.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the Branch entity if found, or null if not.</returns>
        Task<Branch?> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}