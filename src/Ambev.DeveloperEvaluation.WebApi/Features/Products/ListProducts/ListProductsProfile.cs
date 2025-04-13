using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

/// <summary>
/// Profile for mapping ListProducts feature requests, results, and related data transfer objects.
/// </summary>
public class ListProductsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the ListProducts feature.
    /// Maps requests to commands, results to responses, and rating results to rating responses.
    /// </summary>
    public ListProductsProfile()
    {
        /// <summary>
        /// Maps a ListProductsRequest object to a ListProductsCommand object.
        /// </summary>
        CreateMap<ListProductsRequest, ListProductsCommand>();

        /// <summary>
        /// Maps a ListProductsResult object to a ListProductsResponse object.
        /// </summary>
        CreateMap<ListProductsResult, ListProductsResponse>();

        /// <summary>
        /// Maps a ListProductsRatingResult object to a ListProductsRatingResponse object.
        /// </summary>
        CreateMap<ListProductsRatingResult, ListProductsRatingResponse>();
    }
}
