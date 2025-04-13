using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Handler for processing UpdateProductCommand requests.
/// </summary>
/// <remarks>
/// This handler is responsible for validating the incoming command, ensuring the product exists,
/// updating the product details, and returning the updated product information.
/// </remarks>
public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProductHandler"/> class.
    /// </summary>
    /// <param name="productRepository">The repository for managing product data.</param>
    /// <param name="mapper">The mapper for transforming objects between layers.</param>
    public UpdateProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the UpdateProductCommand request.
    /// </summary>
    /// <param name="command">The UpdateProduct command containing the product update details.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation if needed.</param>
    /// <returns>The updated product details encapsulated in <see cref="UpdateProductResult"/>.</returns>
    /// <exception cref="ValidationException">Thrown when the command validation fails.</exception>
    /// <exception cref="ResourceNotFoundException">Thrown when the product to update is not found.</exception>
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        // Validate the command
        var validator = new UpdateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Retrieve the existing product
        var existingProduct = await _productRepository.GetByIdAsync(command.Id, cancellationToken);
        if (existingProduct == null)
            throw new ResourceNotFoundException("Product not found", $"Product with ID {command.Id} not found");

        // Map the updated details to the existing product
        _mapper.Map(command, existingProduct);

        // Update the product in the repository
        var updatedProduct = await _productRepository.UpdateAsync(existingProduct, cancellationToken);

        // Map the updated product to the result object
        var result = _mapper.Map<UpdateProductResult>(updatedProduct);
        return result;
    }
}
