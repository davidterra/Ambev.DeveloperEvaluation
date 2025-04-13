using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.ListCategory;

/// <summary>
/// Profile class for mapping domain entities to DTOs used in the ListCategory feature.
/// </summary>
public class ListCategoryProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListCategoryProfile"/> class.
    /// Configures mappings between <see cref="Product"/> and <see cref="ListCategoryResult"/>,
    /// as well as between <see cref="RatingValue"/> and <see cref="ListCategoryRatingResult"/>.
    /// </summary>
    public ListCategoryProfile()
    {
        // Maps Product to ListCategoryResult, extracting the Price.Amount property.
        CreateMap<Product, ListCategoryResult>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount));

        // Maps RatingValue to ListCategoryRatingResult.
        CreateMap<RatingValue, ListCategoryRatingResult>();
    }
}
