using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Profile for mapping between Application and API GetSale requests and responses.
/// This class defines the AutoMapper configuration for the GetSale feature, 
/// including mappings for commands, requests, results, and responses.
/// </summary>
public class GetSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleProfile"/> class.
    /// Configures mappings for the GetSale feature, including requests, commands, results, and responses.
    /// </summary>
    public GetSaleProfile()
    {
        /// <summary>
        /// Maps an integer ID to a <see cref="GetSaleCommand"/> object.
        /// Constructs the command using the provided ID.
        /// </summary>
        CreateMap<int, GetSaleCommand>()
            .ConstructUsing(id => new GetSaleCommand(id));

        /// <summary>
        /// Maps a <see cref="GetSaleRequest"/> to a <see cref="GetSaleCommand"/>.
        /// </summary>
        CreateMap<GetSaleRequest, GetSaleCommand>();

        /// <summary>
        /// Maps a <see cref="GetSaleResult"/> to a <see cref="GetSaleResponse"/>.
        /// </summary>
        CreateMap<GetSaleResult, GetSaleResponse>();

        /// <summary>
        /// Maps a <see cref="GetSaleItemResult"/> to a <see cref="GetSaleItemResponse"/>.
        /// </summary>
        CreateMap<GetSaleItemResult, GetSaleItemResponse>();
    }
}
