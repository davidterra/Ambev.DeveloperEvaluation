using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Profile for mapping between Application and API UpdateProduct feature.
/// </summary>
public class UpdateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateProduct feature.
    /// Maps requests to commands and results to responses.
    /// </summary>
    public UpdateProductProfile()
    {
        /// <summary>
        /// Maps UpdateProductRequest to UpdateProductCommand.
        /// </summary>
        CreateMap<UpdateProductRequest, UpdateProductCommand>();

        /// <summary>
        /// Maps UpdateRatingRequest to UpdateRatingCommand.
        /// </summary>
        CreateMap<UpdateRatingRequest, UpdateRatingCommand>();

        /// <summary>
        /// Maps UpdateProductResult to UpdateProductResponse.
        /// </summary>
        CreateMap<UpdateProductResult, UpdateProductResponse>();

        /// <summary>
        /// Maps UpdateProductRatingResult to UpdateProductRatingResponse.
        /// </summary>
        CreateMap<UpdateProductRatingResult, UpdateProductRatingResponse>();
    }
}
