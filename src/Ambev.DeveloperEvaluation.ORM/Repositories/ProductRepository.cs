using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Query;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Repository class for managing Product entities in the database.
    /// Provides methods for creating, reading, updating, and deleting products.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        public ProductRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously creates a new product in the database.
        /// </summary>
        /// <param name="product">The product entity to be created.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>The created product entity.</returns>
        public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return product;
        }

        /// <summary>
        /// Asynchronously deletes a product by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the product to be deleted.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(id, cancellationToken);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        /// <summary>
        /// Asynchronously retrieves a product by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the product to be retrieved.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>The product entity if found; otherwise, null.</returns>
        public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a product by its title.
        /// </summary>
        /// <param name="title">The title of the product to be retrieved.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>The product entity if found; otherwise, null.</returns>
        public async Task<Product?> GetByTitleAsync(string title, CancellationToken cancellationToken)
        {
            return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Title == title, cancellationToken);
        }

        /// <summary>
        /// Asynchronously updates an existing product in the database.
        /// </summary>
        /// <param name="product">The product entity to be updated.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>The updated product entity.</returns>
        public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            product.UpdatedAt = DateTime.UtcNow;
            _context.Products.Attach(product);
            _context.Entry(product).State = EntityState.Modified;
            _context.Entry(product.Rating).State = EntityState.Modified;

            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }

        /// <summary>
        /// Retrieves a queryable list of products with optional sorting and filtering.
        /// </summary>
        /// <param name="orderBy">The field to order the products by.</param>
        /// <param name="filters">Optional filters to apply to the product list.</param>
        /// <returns>An IQueryable of products.</returns>
        public IQueryable<Product> ListProducts(string orderBy, Dictionary<string, string>? filters)
        {
            var products = _context.Products.AsQueryable();

            var query = new QueryFilterAndSorter<Product>(products, orderBy, filters);
            return query.Apply();
        }

        /// <summary>
        /// Asynchronously retrieves a distinct list of product categories.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>A list of unique product categories.</returns>
        public async Task<List<string>> ListCategoriesAsync(CancellationToken cancellationToken)
        {
            return await _context.Products
                .AsNoTracking()
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Retrieves a queryable list of products filtered by category with optional sorting and filtering.
        /// </summary>
        /// <param name="category">The category to filter products by.</param>
        /// <param name="orderBy">The field to order the products by.</param>
        /// <param name="filters">Optional filters to apply to the product list.</param>
        /// <returns>An IQueryable of products filtered by category.</returns>
        public IQueryable<Product> ListCategory(string category, string orderBy, Dictionary<string, string>? filters)
        {
            var products = _context.Products.AsQueryable();

            products = products.Where(p => p.Category == category);

            var query = new QueryFilterAndSorter<Product>(products, orderBy, filters);
            return query.Apply();
        }

        /// <summary>
        /// Asynchronously retrieves a list of products by their identifiers.
        /// </summary>
        /// <param name="ids">An array of product identifiers.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>A list of products matching the specified identifiers.</returns>
        public async Task<List<Product>> GetByIdsAsync(int[] ids, CancellationToken cancellationToken)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => ids.Contains(p.Id))
                .ToListAsync(cancellationToken);
        }
    }
}
