using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Command for retrieving details of a specific sale.
/// </summary>
/// <remarks>
/// This command is used to fetch the details of a sale based on its unique identifier.
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="GetSaleResult"/> containing the sale's details.
/// </remarks>
public class GetSaleCommand : IRequest<GetSaleResult>
{
    /// <summary>
    /// Gets the identifier of the sale to be retrieved.
    /// </summary>
    /// <value>An integer representing the sale ID.</value>
    public int Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleCommand"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the sale to retrieve.</param>
    public GetSaleCommand(int id)
    {
        Id = id;
    }
}
