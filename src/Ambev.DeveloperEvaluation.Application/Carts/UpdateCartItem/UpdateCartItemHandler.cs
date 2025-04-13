using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCartItem;

/// <summary>
/// Handles the update of a cart item by processing the command, validating it, 
/// applying discounts, and updating the cart product in the repository.
/// </summary>
public class UpdateCartItemHandler : IRequestHandler<UpdateCartItemCommand, UpdateCartItemResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IDiscountService _discountService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCartItemHandler"/> class.
    /// </summary>
    /// <param name="cartRepository">The repository for cart operations.</param>
    /// <param name="discountService">The service for applying discounts to cart products.</param>
    /// <param name="mapper">The mapper for converting domain objects to result objects.</param>
    public UpdateCartItemHandler(ICartRepository cartRepository,
        IDiscountService discountService,
        IMapper mapper)
    {
        _cartRepository = cartRepository;
        _discountService = discountService;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the update cart item command.
    /// </summary>
    /// <param name="command">The command containing the cart item update details.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The result of the updated cart item.</returns>
    /// <exception cref="ValidationException">Thrown when the command validation fails.</exception>
    /// <exception cref="ResourceNotFoundException">Thrown when the cart product is not found.</exception>
    public async Task<UpdateCartItemResult> Handle(UpdateCartItemCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateCartItemCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cartProduct = await _cartRepository.GetCartProductByIdAsync(command.Id, cancellationToken);
        if (cartProduct == null)
        {
            throw new ResourceNotFoundException("Cart product not found", $"Cart product with ID {command.Id} not found");
        }

        cartProduct.Quantity = command.Quantity;

        _discountService.ApplyDiscount(cartProduct);

        cartProduct.SetLastUpdated();

        var updatedCart = await _cartRepository.UpdateCartProductAsync(cartProduct, cancellationToken);
        var result = _mapper.Map<UpdateCartItemResult>(updatedCart);

        return result;
    }
}
