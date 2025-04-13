using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Profile for mapping between Application and API UpdateSale requests and responses.
/// </summary>
public class UpdateSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleProfile"/> class.
    /// Configures mappings for the UpdateSale feature, including requests, commands, results, and responses.
    /// </summary>
    public UpdateSaleProfile()
    {
        // Maps UpdateSaleRequest to UpdateSaleCommand.
        CreateMap<UpdateSaleRequest, UpdateSaleCommand>();

        // Maps UpdateSaleResult to UpdateSaleResponse.
        CreateMap<UpdateSaleResult, UpdateSaleResponse>();

        // Maps UpdateSaleItemResult to UpdateSaleItemResponse.
        CreateMap<UpdateSaleItemResult, UpdateSaleItemResponse>();
    }
}
