using Ambev.DeveloperEvaluation.Application.Products.ListCategory;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListCategory;

/// <summary>
/// Profile for mapping ListCategory feature requests, results, and ratings to their respective commands and responses.
/// </summary>
public class ListCategoryProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListCategoryProfile"/> class.
    /// Configures mappings between request and command, result and response, and rating result and rating response for the ListCategory feature.
    /// </summary>
    public ListCategoryProfile()
    {
        /// <summary>
        /// Maps <see cref="ListCategoryRequest"/> to <see cref="ListCategoryCommand"/>.
        /// </summary>
        CreateMap<ListCategoryRequest, ListCategoryCommand>();

        /// <summary>
        /// Maps <see cref="ListCategoryResult"/> to <see cref="ListCategoryResponse"/>.
        /// </summary>
        CreateMap<ListCategoryResult, ListCategoryResponse>();

        /// <summary>
        /// Maps <see cref="ListCategoryRatingResult"/> to <see cref="ListCategoryRatingResponse"/>.
        /// </summary>
        CreateMap<ListCategoryRatingResult, ListCategoryRatingResponse>();
    }
}
