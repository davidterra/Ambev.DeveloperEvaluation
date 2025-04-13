using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

/// <summary>
/// Profile for mapping between Product entity and ListProductsResult.
/// </summary>
public class ListProductsProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListProductsProfile"/> class.
    /// Configures the mappings for the ListProducts operation.
    /// </summary>
    public ListProductsProfile()
    {
        // Maps Product entity to ListProductsResult, mapping the Price property to its Amount value.
        CreateMap<Product, ListProductsResult>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount));

        // Maps RatingValue value object to ListProductsRatingResult.
        CreateMap<RatingValue, ListProductsRatingResult>();
    }
}
