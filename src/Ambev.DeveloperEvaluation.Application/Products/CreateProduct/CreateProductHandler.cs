using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Handler for processing CreateProductCommand requests.
/// </summary>
/// <remarks>
/// This handler is responsible for validating the incoming command, checking for duplicate products,
/// mapping the command to a domain entity, and persisting the new product in the repository.
/// It uses AutoMapper for object mapping and FluentValidation for validation.
/// </remarks>
public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductHandler"/> class.
    /// </summary>
    /// <param name="productRepository">The repository for managing product data.</param>
    /// <param name="mapper">The mapper for converting between models and entities.</param>
    public CreateProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateProductCommand request.
    /// </summary>
    /// <param name="command">The CreateProduct command containing product details.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation if needed.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created product details.</returns>
    /// <exception cref="ValidationException">Thrown when the command validation fails or a product with the same title already exists.</exception>
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingProduct = await _productRepository.GetByTitleAsync(command.Title, cancellationToken);
        if (existingProduct != null)
            throw new ValidationException([new("Title", $"Product with title {command.Title} already exists") { ErrorCode = "Invalid input data" }]);

        var product = _mapper.Map<Product>(command);

        var createdProduct = await _productRepository.CreateAsync(product, cancellationToken);
        var result = _mapper.Map<CreateProductResult>(createdProduct);
        return result;
    }
}
