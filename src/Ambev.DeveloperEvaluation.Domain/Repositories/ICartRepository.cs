using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for cart repository operations, including CRUD operations
    /// and specific queries related to carts and cart products.
    /// </summary>
    public interface ICartRepository
    {
        /// <summary>
        /// Adds a product to a cart asynchronously.
        /// </summary>
        /// <param name="cartProduct">The cart product to add.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The added cart product.</returns>
        Task<CartItem> AddProductToCartAsync(CartItem cartProduct, CancellationToken cancellationToken);

        /// <summary>
        /// Creates a new cart asynchronously.
        /// </summary>
        /// <param name="cart">The cart to create.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The created cart.</returns>
        Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a cart by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the cart.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The cart if found; otherwise, null.</returns>
        Task<Cart?> GetByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a cart product by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the cart product.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The cart product if found; otherwise, null.</returns>
        Task<CartItem?> GetCartProductByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Lists all carts with optional ordering and filtering.
        /// </summary>
        /// <param name="orderBy">The field to order by.</param>
        /// <param name="filters">Optional filters to apply.</param>
        /// <returns>An IQueryable of carts.</returns>
        IQueryable<Cart> ListCarts(string orderBy, Dictionary<string, string>? filters);

        /// <summary>
        /// Updates an existing cart asynchronously.
        /// </summary>
        /// <param name="cart">The cart to update.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The updated cart.</returns>
        Task<Cart> UpdateAsync(Cart cart, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an existing cart product asynchronously.
        /// </summary>
        /// <param name="cartProduct">The cart product to update.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The updated cart product.</returns>
        Task<CartItem> UpdateCartProductAsync(CartItem cartProduct, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves the active cart for a specific user and branch asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>        
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The active cart if found; otherwise, null.</returns>
        Task<Cart?> GetActiveCartForUserAsync(Guid userId, CancellationToken cancellationToken);
    }
}