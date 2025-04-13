using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Interface for managing product-related data operations.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Creates a new product asynchronously.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The created product.</returns>
        Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a product by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the product to delete.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>True if the product was deleted; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a product by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the product to retrieve.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The product if found; otherwise, null.</returns>
        Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves multiple products by their identifiers asynchronously.
        /// </summary>
        /// <param name="ids">The array of product identifiers.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A list of products matching the provided identifiers.</returns>
        Task<List<Product>> GetByIdsAsync(int[] ids, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a product by its title asynchronously.
        /// </summary>
        /// <param name="title">The title of the product to retrieve.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The product if found; otherwise, null.</returns>
        Task<Product?> GetByTitleAsync(string title, CancellationToken cancellationToken);

        /// <summary>
        /// Lists all unique product categories asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A list of unique product categories.</returns>
        Task<List<string>> ListCategoriesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Lists products in a specific category with optional ordering and filtering.
        /// </summary>
        /// <param name="category">The category to filter products by.</param>
        /// <param name="orderBy">The field to order the products by.</param>
        /// <param name="filters">Optional filters to apply to the product list.</param>
        /// <returns>An IQueryable of products matching the criteria.</returns>
        IQueryable<Product> ListCategory(string category, string orderBy, Dictionary<string, string>? filters);

        /// <summary>
        /// Lists all products with optional ordering and filtering.
        /// </summary>
        /// <param name="orderBy">The field to order the products by.</param>
        /// <param name="filters">Optional filters to apply to the product list.</param>
        /// <returns>An IQueryable of products matching the criteria.</returns>
        IQueryable<Product> ListProducts(string orderBy, Dictionary<string, string>? filters);

        /// <summary>
        /// Updates an existing product asynchronously.
        /// </summary>
        /// <param name="product">The product to update.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The updated product.</returns>
        Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken);
    }
}