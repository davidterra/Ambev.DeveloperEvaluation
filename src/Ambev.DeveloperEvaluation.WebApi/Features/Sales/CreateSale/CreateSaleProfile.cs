using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Profile for mapping between Application and API CreateSale requests and responses.
/// This class is responsible for configuring AutoMapper mappings for the CreateSale feature,
/// ensuring seamless data transformation between layers.
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleProfile"/> class.
    /// Configures mappings for the CreateSale feature, including requests, commands, results, and responses.
    /// </summary>
    public CreateSaleProfile()
    {
        /// <summary>
        /// Maps <see cref="CreateSaleRequest"/> to <see cref="CreateSaleCommand"/>.
        /// This mapping is used to transform API requests into application commands.
        /// </summary>
        CreateMap<CreateSaleRequest, CreateSaleCommand>();

        /// <summary>
        /// Maps <see cref="CreateSaleResult"/> to <see cref="CreateSaleResponse"/>.
        /// This mapping is used to transform application results into API responses.
        /// </summary>
        CreateMap<CreateSaleResult, CreateSaleResponse>();

        /// <summary>
        /// Maps <see cref="CreateSaleItemResult"/> to <see cref="CreateSaleItemResponse"/>.
        /// This mapping is used to transform individual sale item results into API responses.
        /// </summary>
        CreateMap<CreateSaleItemResult, CreateSaleItemResponse>();
    }
}
