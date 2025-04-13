using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Profile for mapping between Product-related entities and DTOs for the UpdateProduct operation.
/// </summary>
public class UpdateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the UpdateProduct operation.
    /// Maps UpdateProductCommand to Product, Product to UpdateProductResult, and RatingValue to UpdateProductRatingResult.
    /// </summary>
    public UpdateProductProfile()
    {
        // Maps UpdateProductCommand to Product, including custom mapping for Rating.
        CreateMap<UpdateProductCommand, Product>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new RatingValue(src.Rating.Rate, src.Rating.Count)));

        // Maps Product to UpdateProductResult, including custom mapping for Price.
        CreateMap<Product, UpdateProductResult>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount));

        // Maps RatingValue to UpdateProductRatingResult directly.
        CreateMap<RatingValue, UpdateProductRatingResult>();
    }
}
