using Ambev.DeveloperEvaluation.Application.Sales.DeleteSaleItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSaleItem;

/// <summary>
/// Profile for mapping DeleteSaleItem feature requests to commands.
/// This class is responsible for configuring the mapping between a tuple of integers and the DeleteSaleItemCommand.
/// </summary>
public class DeleteSaleItemProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleItemProfile"/> class.
    /// Configures the mapping for the DeleteSaleItem feature.
    /// </summary>
    public DeleteSaleItemProfile()
    {
        /// <summary>
        /// Maps a tuple of integers (representing sale and item IDs) to a DeleteSaleItemCommand.
        /// Constructs the command using the provided tuple values.
        /// </summary>
        CreateMap<(int, int), DeleteSaleItemCommand>()
            .ConstructUsing(t => new DeleteSaleItemCommand(t.Item1, t.Item2));
    }
}
