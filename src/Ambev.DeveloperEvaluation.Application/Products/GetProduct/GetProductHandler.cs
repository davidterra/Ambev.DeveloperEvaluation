using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Handler for processing GetProductCommand requests.
/// Retrieves product details by ID, validates the request, and maps the result.
/// </summary>
public class GetProductHandler : IRequestHandler<GetProductCommand, GetProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductHandler"/> class.
    /// </summary>
    /// <param name="productRepository">The product repository for accessing product data.</param>
    /// <param name="mapper">The AutoMapper instance for mapping domain models to result models.</param>
    public GetProductHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetProductCommand request.
    /// Validates the command, retrieves the product by ID, and maps it to the result model.
    /// </summary>
    /// <param name="request">The GetProduct command containing the product ID.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation if needed.</param>
    /// <returns>The product details if found, otherwise throws an exception.</returns>
    /// <exception cref="ValidationException">Thrown when the command validation fails.</exception>
    /// <exception cref="ResourceNotFoundException">Thrown when the product is not found.</exception>
    public async Task<GetProductResult> Handle(GetProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetProductValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new ResourceNotFoundException("Product not found", $"Product with ID {request.Id} not found");

        return _mapper.Map<GetProductResult>(product);
    }
}
