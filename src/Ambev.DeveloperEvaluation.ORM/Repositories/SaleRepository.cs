using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Repository implementation for managing Sale entities in the database.
/// Provides methods to create, update, and retrieve Sale and SaleItem data.
/// </summary>
public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="SaleRepository"/> class.
    /// </summary>
    /// <param name="context">The database context used for data access.</param>
    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new Sale entity in the database.
    /// </summary>
    /// <param name="sale">The Sale entity to create.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the created Sale entity.
    /// </returns>
    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken)
    {
        _context.Sales.Add(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    /// <summary>
    /// Updates an existing Sale entity in the database.
    /// </summary>
    /// <param name="sale">The Sale entity to update.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the updated Sale entity.
    /// </returns>
    public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        _context.Sales.Attach(sale);
        _context.Entry(sale).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    /// <summary>
    /// Updates an existing SaleItem entity in the database.
    /// </summary>
    /// <param name="item">The SaleItem entity to update.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the updated SaleItem entity.
    /// </returns>
    public async Task<SaleItem> UpdateItemAsync(SaleItem item, CancellationToken cancellationToken = default)
    {
        _context.SaleItems.Attach(item);
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return item;
    }

    /// <summary>
    /// Retrieves a Sale entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Sale.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the Sale entity
    /// if found; otherwise, null.
    /// </returns>
    public async Task<Sale?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var sale = await _context.Sales
            .Include(x => x.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        return sale;
    }
}
