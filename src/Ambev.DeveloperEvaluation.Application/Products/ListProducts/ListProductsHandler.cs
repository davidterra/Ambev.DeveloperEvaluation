using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

/// <summary>
/// Handler for processing ListProductsCommand requests.
/// This handler validates the incoming request, retrieves the list of products from the repository,
/// and maps the results to the ListProductsResult model.
/// </summary>
public class ListProductsHandler : IRequestHandler<ListProductsCommand, IQueryable<ListProductsResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListProductsHandler"/> class.
    /// </summary>
    /// <param name="productRepository">The product repository to interact with product data.</param>
    /// <param name="mapper">The AutoMapper instance for mapping domain models to result models.</param>
    public ListProductsHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the ListProductsCommand request.
    /// Validates the request, retrieves the products from the repository, and maps them to the result model.
    /// </summary>
    /// <param name="request">The ListProductsCommand request containing pagination, sorting, and filtering options.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A queryable list of <see cref="ListProductsResult"/>.</returns>
    /// <exception cref="ValidationException">Thrown when the request fails validation.</exception>
    public async Task<IQueryable<ListProductsResult>> Handle(ListProductsCommand request, CancellationToken cancellationToken)
    {
        var validator = new ListProductsCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var products = _productRepository.ListProducts(request.OrderBy, request.Filters);
        var result = products.ProjectTo<ListProductsResult>(_mapper.ConfigurationProvider);

        return result;
    }
}
