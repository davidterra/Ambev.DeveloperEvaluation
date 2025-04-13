using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

/// <summary>
/// Profile for mapping DeleteSale feature requests to commands.
/// This class is responsible for configuring the mapping between an integer ID and the DeleteSaleCommand.
/// </summary>
public class DeleteSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleProfile"/> class.
    /// Configures the mapping for the DeleteSale feature.
    /// </summary>
    public DeleteSaleProfile()
    {
        // Maps an integer ID to a DeleteSaleCommand, constructing the command using the provided ID.
        CreateMap<int, DeleteSaleCommand>()
            .ConstructUsing(id => new DeleteSaleCommand(id));
    }
}
