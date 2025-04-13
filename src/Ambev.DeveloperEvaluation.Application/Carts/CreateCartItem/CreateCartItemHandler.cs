using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCartItem;

/// <summary>
/// Handles the creation of cart items by processing the command and interacting with repositories and services.
/// </summary>
public class CreateCartItemHandler : IRequestHandler<CreateCartItemCommand, CreateCartItemResult>
{
    /// <summary>
    /// Repository for accessing product data.
    /// </summary>
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Repository for accessing user data.
    /// </summary>
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Repository for accessing cart data.
    /// </summary>
    private readonly ICartRepository _cartRepository;


    /// <summary>
    /// Service for applying discounts to cart products.
    /// </summary>
    private readonly IDiscountService _discountService;
    
    /// <summary>
    /// Mapper for transforming objects between different models.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartItemHandler"/> class.
    /// </summary>
    /// <param name="productRepository">The product repository.</param>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="cartRepository">The cart repository.</param>    
    /// <param name="discountService">The discount service.</param>
    /// <param name="mapper">The mapper for object transformations.</param>
    public CreateCartItemHandler(
        IProductRepository productRepository,
        IUserRepository userRepository,
        ICartRepository cartRepository,
        IDiscountService discountService,        
        IMapper mapper)
    {
        _productRepository = productRepository;
        _userRepository = userRepository;
        _cartRepository = cartRepository;
        _discountService = discountService;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the creation of a cart item by validating the command, retrieving necessary data, and updating the cart.
    /// </summary>
    /// <param name="command">The command containing cart item details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the cart item creation.</returns>
    public async Task<CreateCartItemResult> Handle(CreateCartItemCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await ValidateCommandAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingUser = await GetUserAsync(command.UserId, cancellationToken);
        var existingProduct = await GetProductAsync(command.Product.ProductId, cancellationToken);

        var existingCart = await _cartRepository.GetActiveCartForUserAsync(command.UserId, cancellationToken);

        return existingCart == null
            ? await CreateNewCartAsync(command, existingProduct, cancellationToken)
            : await AddProductToExistingCartAsync(existingCart, command, existingProduct, cancellationToken);
    }

    /// <summary>
    /// Validates the command using the <see cref="CreateCartItemCommandValidator"/>.
    /// </summary>
    /// <param name="command">The command to validate.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The validation result.</returns>
    private static async Task<ValidationResult> ValidateCommandAsync(CreateCartItemCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateCartItemCommandValidator();
        return await validator.ValidateAsync(command, cancellationToken);
    }

    /// <summary>
    /// Retrieves a user by their ID and validates their existence.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The user entity.</returns>
    private async Task<User> GetUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (existingUser == null)
            throw new ValidationException(new[] { new ValidationFailure("UserId", $"User with ID {userId} does not exist") { ErrorCode = "Invalid input data" } });

        return existingUser;
    }

    /// <summary>
    /// Retrieves a product by its ID and validates its existence.
    /// </summary>
    /// <param name="productId">The product ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The product entity.</returns>
    private async Task<Product> GetProductAsync(int productId, CancellationToken cancellationToken)
    {
        var existingProduct = await _productRepository.GetByIdAsync(productId, cancellationToken);

        if (existingProduct == null)
            throw new ValidationException(new[] { new ValidationFailure("ProductId", $"Product with ID {productId} does not exist") { ErrorCode = "Invalid input data" } });

        return existingProduct;
    }

    /// <summary>
    /// Creates a new cart and adds the product to it.
    /// </summary>
    /// <param name="command">The command containing cart item details.</param>
    /// <param name="product">The product to add to the cart.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the cart creation.</returns>
    private async Task<CreateCartItemResult> CreateNewCartAsync(CreateCartItemCommand command, Product product, CancellationToken cancellationToken)
    {
        var cart = CreateCart(command, product);
        var createdCart = await _cartRepository.CreateAsync(cart, cancellationToken);
        
        return new CreateCartItemResult
        {
            Id = createdCart.Id,
            UserId = createdCart.UserId,
            Product = _mapper.Map<CreateItemCartProductResult>(createdCart.Items.First())
        };
    }

    /// <summary>
    /// Adds a product to an existing cart.
    /// </summary>
    /// <param name="existingCart">The existing cart.</param>
    /// <param name="command">The command containing cart item details.</param>
    /// <param name="product">The product to add to the cart.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the cart update.</returns>
    private async Task<CreateCartItemResult> AddProductToExistingCartAsync(Cart existingCart, CreateCartItemCommand command, Product product, CancellationToken cancellationToken)
    {
        var cartProduct = CreateCartProduct(existingCart, command, product);
        var updatedCart = await _cartRepository.AddProductToCartAsync(cartProduct, cancellationToken);
        
        return new CreateCartItemResult
        {
            Id = existingCart.Id,
            UserId = existingCart.UserId,
            Product = _mapper.Map<CreateItemCartProductResult>(updatedCart)
        };
    }

    /// <summary>
    /// Creates a new cart entity with the specified product.
    /// </summary>
    /// <param name="command">The command containing cart item details.</param>
    /// <param name="product">The product to add to the cart.</param>
    /// <returns>The created cart entity.</returns>
    private Cart CreateCart(CreateCartItemCommand command, Product product)
    {
        var cartProduct = _mapper.Map<CartItem>(command.Product);
        cartProduct.UnitPrice = product.Price;
        _discountService.ApplyDiscount(cartProduct);
        cartProduct.Subtotal();

        return new Cart
        {
            UserId = command.UserId,
            Items = new List<CartItem> { cartProduct }
        };
    }

    /// <summary>
    /// Creates a new cart product entity and applies a discount.
    /// </summary>
    /// <param name="cart">The cart to which the product belongs.</param>
    /// <param name="command">The command containing cart item details.</param>
    /// <param name="product">The product to add to the cart.</param>
    /// <returns>The created cart product entity.</returns>
    private CartItem CreateCartProduct(Cart cart, CreateCartItemCommand command, Product product)
    {
        var productInCart = cart.Items.FirstOrDefault(p => p.ProductId == command.Product.ProductId);
        if (productInCart != null)
        {
            throw new ValidationException(new[] { new ValidationFailure("ProductId", $"Product with ID {product.Id} already exists in the cart") { ErrorCode = "Invalid input data" } });
        }

        var cartItem = new CartItem
        {
            CartId = cart.Id,
            ProductId = product.Id,
            Quantity = command.Product.Quantity,
            UnitPrice = product.Price
        };

        _discountService.ApplyDiscount(cartItem);
        cartItem.Subtotal();
        return cartItem;
    }
}
