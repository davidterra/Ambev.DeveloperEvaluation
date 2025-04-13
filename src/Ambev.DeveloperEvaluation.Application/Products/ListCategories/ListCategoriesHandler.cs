using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListCategories;

/// <summary>
/// Handler for processing ListCategoriesCommand requests.
/// This handler retrieves a list of product categories from the repository
/// and maps them into a result object to be returned.
/// </summary>
public class ListCategoriesHandler : IRequestHandler<ListCategoriesCommand, ListCategoriesResult>
{
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListCategoriesHandler"/> class.
    /// </summary>
    /// <param name="productRepository">The product repository used to access product data.</param>    
    public ListCategoriesHandler(
        IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    /// <summary>
    /// Handles the ListCategoriesCommand request.
    /// This method fetches the list of categories from the repository
    /// and returns them as a <see cref="ListCategoriesResult"/>.
    /// </summary>
    /// <param name="request">The ListCategories command containing the request details.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of categories.</returns>
    public async Task<ListCategoriesResult> Handle(ListCategoriesCommand request, CancellationToken cancellationToken)
    {
        var categories = await _productRepository.ListCategoriesAsync(cancellationToken);

        var result = new ListCategoriesResult();
        result.AddRange(categories);

        return result;
    }
}
