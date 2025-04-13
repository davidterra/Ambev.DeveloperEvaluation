using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListCategory;


/// <summary>
/// Handles the ListCategoryCommand to retrieve a list of products within a specific category.
/// </summary>
public class ListCategoryHandler : IRequestHandler<ListCategoryCommand, IQueryable<ListCategoryResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListCategoryHandler"/> class.
    /// </summary>
    /// <param name="productRepository">The repository to access product data.</param>
    /// <param name="mapper">The mapper to project data into the desired result format.</param>
    public ListCategoryHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the ListCategoryCommand to validate the request and retrieve the filtered and ordered list of products.
    /// </summary>
    /// <param name="request">The command containing category, order, and filter details.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation if needed.</param>
    /// <returns>A queryable collection of <see cref="ListCategoryResult"/>.</returns>
    /// <exception cref="ValidationException">Thrown when the request validation fails.</exception>
    public async Task<IQueryable<ListCategoryResult>> Handle(ListCategoryCommand request, CancellationToken cancellationToken)
    {
        var validator = new ListCategoryValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var categoryProducts = _productRepository.ListCategory(request.Category, request.OrderBy, request.Filters);

        return _mapper.ProjectTo<ListCategoryResult>(categoryProducts, cancellationToken);
    }
}
