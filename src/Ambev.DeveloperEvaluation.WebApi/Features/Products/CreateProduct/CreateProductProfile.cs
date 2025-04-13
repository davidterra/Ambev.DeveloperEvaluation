using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Profile for mapping between Application and API CreateProduct responses.
/// </summary>
public class UpdateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the CreateProduct feature.
    /// Maps requests and results between the API and Application layers.
    /// </summary>
    public UpdateProductProfile()
    {
        /// <summary>
        /// Maps CreateProductRequest to CreateProductCommand.
        /// </summary>
        CreateMap<CreateProductRequest, CreateProductCommand>();

        /// <summary>
        /// Maps CreateRatingRequest to CreateRatingCommand.
        /// </summary>
        CreateMap<CreateRatingRequest, CreateRatingCommand>();

        /// <summary>
        /// Maps CreateProductResult to CreateProductResponse.
        /// </summary>
        CreateMap<CreateProductResult, CreateProductResponse>();

        /// <summary>
        /// Maps CreateProductRatingResult to CreateProductRatingResponse.
        /// </summary>
        CreateMap<CreateProductRatingResult, CreateProductRatingResponse>();
    }
}
