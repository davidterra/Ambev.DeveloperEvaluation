using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Handler for processing DeleteProductCommand requests.
/// This handler validates the command, ensures the product exists, and deletes it from the repository.
/// </summary>
public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResponse>
{
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Initializes a new instance of DeleteProductHandler.
    /// </summary>
    /// <param name="productRepository">The product repository used to manage product data.</param>
    public DeleteProductHandler(
        IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    /// <summary>
    /// Handles the DeleteProductCommand request.
    /// Validates the command, checks for foreign key constraints, and deletes the product.
    /// </summary>
    /// <param name="request">The DeleteProduct command containing the product ID to delete.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation if needed.</param>
    /// <returns>The result of the delete operation, indicating success or failure.</returns>
    /// <exception cref="ValidationException">Thrown when the command validation fails.</exception>
    /// <exception cref="ResourceNotFoundException">Thrown when the product to delete is not found.</exception>
    public async Task<DeleteProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteProductValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // todo: valid FK constraint

        var success = await _productRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new ResourceNotFoundException("Product not found", $"Product with ID {request.Id} not found");

        return new DeleteProductResponse { Success = true };
    }
}
