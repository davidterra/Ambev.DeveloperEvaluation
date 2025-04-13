using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Represents a repository interface for managing Branch entities.
    /// Provides methods to retrieve Branch data from the data source.
    /// </summary>
    public interface ISaleRepository
    {
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken);
        Task<Sale?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);
        Task<SaleItem> UpdateItemAsync(SaleItem item, CancellationToken cancellationToken = default);
    }
}