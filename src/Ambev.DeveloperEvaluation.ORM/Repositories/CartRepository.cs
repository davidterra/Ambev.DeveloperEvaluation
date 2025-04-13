using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Query;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Repository for managing Cart and CartProduct entities in the database.
    /// Provides methods for CRUD operations and querying carts and their products.
    /// </summary>
    public class CartRepository : ICartRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartRepository"/> class.
        /// </summary>
        /// <param name="context">The database context to be used for operations.</param>
        public CartRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new cart in the database.
        /// </summary>
        /// <param name="cart">The cart entity to be created.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The created cart entity.</returns>
        public async Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken)
        {
            await _context.Carts.AddAsync(cart, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return cart;
        }

        /// <summary>
        /// Retrieves a cart by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the cart.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The cart entity if found; otherwise, null.</returns>
        public async Task<Cart?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
            return cart;
        }

        /// <summary>
        /// Retrieves a cart product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the cart product.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The cart product entity if found; otherwise, null.</returns>
        public async Task<CartItem?> GetCartProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            var cartProduct = await _context.CartItems
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
            return cartProduct;
        }

        /// <summary>
        /// Lists carts with optional filtering and sorting.
        /// </summary>
        /// <param name="orderBy">The field to order the results by.</param>
        /// <param name="filters">Optional filters to apply to the query.</param>
        /// <returns>An IQueryable of carts matching the criteria.</returns>
        public IQueryable<Cart> ListCarts(string orderBy, Dictionary<string, string>? filters)
        {

            var carts = _context.Carts
                .Include(x => x.Items)
                .AsQueryable();
            
            var query = new QueryFilterAndSorter<Cart>(carts, orderBy, filters);
            return query.Apply();
        }

        /// <summary>
        /// Updates an existing cart in the database.
        /// </summary>
        /// <param name="cart">The cart entity to be updated.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The updated cart entity.</returns>
        public async Task<Cart> UpdateAsync(Cart cart, CancellationToken cancellationToken)
        {
            _context.Carts.Attach(cart);
            _context.Entry(cart).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return cart;
        }

        /// <summary>
        /// Updates an existing cart product in the database.
        /// </summary>
        /// <param name="cartProduct">The cart product entity to be updated.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The updated cart product entity.</returns>
        public async Task<CartItem> UpdateCartProductAsync(CartItem cartProduct, CancellationToken cancellationToken)
        {
            _context.CartItems.Attach(cartProduct);
            _context.Entry(cartProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return cartProduct;
        }

        /// <summary>
        /// Adds a product to a cart in the database.
        /// </summary>
        /// <param name="cartProduct">The cart product entity to be added.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The added cart product entity.</returns>
        public async Task<CartItem> AddProductToCartAsync(CartItem cartProduct, CancellationToken cancellationToken)
        {
            await _context.CartItems.AddAsync(cartProduct, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return cartProduct;
        }

        /// <summary>
        /// Retrieves the active cart for a specific user and branch.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The active cart entity if found; otherwise, null.</returns>
        public async Task<Cart?> GetActiveCartForUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == userId
                    && c.Status == CartStatus.Active, cancellationToken);
            return cart;
        }
    }
}
