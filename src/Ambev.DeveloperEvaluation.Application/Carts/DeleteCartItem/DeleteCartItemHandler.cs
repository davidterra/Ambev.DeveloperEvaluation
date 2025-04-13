using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCartItem;

/// <summary>
/// Handler for processing DeleteCartCommand requests.
/// This class is responsible for handling the deletion of a cart item by validating the request,
/// retrieving the cart item, performing the deletion, and notifying other parts of the system.
/// </summary>
public class DeleteCartItemHandler : IRequestHandler<DeleteCartItemCommand, DeleteCartItemResponse>
{
    private readonly ICartRepository _cartRepository;

    /// <summary>
    /// Initializes a new instance of DeleteCartItemHandler.
    /// </summary>
    /// <param name="cartRepository">The cart repository used to interact with cart data.</param>
    public DeleteCartItemHandler(
        ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    /// <summary>
    /// Handles the DeleteCartCommand request.
    /// Validates the request, retrieves the cart item, deletes it, updates the repository,
    /// and publishes a notification about the deletion.
    /// </summary>
    /// <param name="request">The DeleteCartItemCommand containing the ID of the cart item to delete.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation if needed.</param>
    /// <returns>A DeleteCartItemResponse indicating the success of the operation.</returns>
    /// <exception cref="ValidationException">Thrown if the request validation fails.</exception>
    /// <exception cref="ResourceNotFoundException">Thrown if the cart item is not found.</exception>
    public async Task<DeleteCartItemResponse> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteCartItemValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cartProduct = await _cartRepository.GetCartProductByIdAsync(request.Id, cancellationToken);
        if (cartProduct == null)
            throw new ResourceNotFoundException("Cart item not found", $"Cart item with ID {request.Id} not found");

        cartProduct.SetAsCanceled();

        var updateCartProduct = await _cartRepository.UpdateCartProductAsync(cartProduct, cancellationToken);        

        return new DeleteCartItemResponse { Success = true };
    }
}
