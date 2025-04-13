using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

/// <summary>
/// Handler for processing DeleteCartCommand requests.
/// This class is responsible for validating the command, retrieving the cart,
/// marking it as deleted, updating the repository, and publishing a notification
/// about the sale cancellation.
/// </summary>
public class DeleteCartHandler : IRequestHandler<DeleteCartCommand, DeleteCartResponse>
{
    private readonly ICartRepository _cartRepository;

    /// <summary>
    /// Initializes a new instance of DeleteCartHandler.
    /// </summary>
    /// <param name="cartRepository">The cart repository to interact with cart data.</param>    
    public DeleteCartHandler(
        ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;        
    }

    /// <summary>
    /// Handles the DeleteCartCommand request.
    /// Validates the command, retrieves the cart, marks it as deleted,
    /// updates the repository, and publishes a SaleCancelledNotification.
    /// </summary>
    /// <param name="request">The DeleteCart command containing the cart ID to delete.</param>
    /// <param name="cancellationToken">Cancellation token to handle request cancellation.</param>
    /// <returns>The result of the delete operation as a DeleteCartResponse.</returns>
    /// <exception cref="ValidationException">Thrown when the command validation fails.</exception>
    /// <exception cref="ResourceNotFoundException">Thrown when the cart is not found.</exception>
    public async Task<DeleteCartResponse> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteCartValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cart = await _cartRepository.GetByIdAsync(request.Id, cancellationToken);

        if (cart == null)
        {
            throw new ResourceNotFoundException("Cart not found", $"Cart with ID {request.Id} not found");
        }

        cart.SetAsCanceled();

        await _cartRepository.UpdateAsync(cart, cancellationToken);
        
        return new DeleteCartResponse { Success = true };
    }
}
