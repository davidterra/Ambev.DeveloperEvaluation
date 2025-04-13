using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// Profile for mapping GetProduct feature requests, results, and responses.
/// </summary>
public class GetProductProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductProfile"/> class.
    /// Configures mappings for the GetProduct feature.
    /// </summary>
    public GetProductProfile()
    {
        /// <summary>
        /// Maps an integer ID to a <see cref="GetProductCommand"/> instance.
        /// </summary>
        CreateMap<int, GetProductCommand>()
            .ConstructUsing(id => new GetProductCommand(id));

        /// <summary>
        /// Maps a <see cref="GetProductResult"/> to a <see cref="GetProductResponse"/>.
        /// </summary>
        CreateMap<GetProductResult, GetProductResponse>();

        /// <summary>
        /// Maps a <see cref="GetProductRatingResult"/> to a <see cref="GetProductRatingResponse"/>.
        /// </summary>
        CreateMap<GetProductRatingResult, GetProductRatingResponse>();
    }
}
