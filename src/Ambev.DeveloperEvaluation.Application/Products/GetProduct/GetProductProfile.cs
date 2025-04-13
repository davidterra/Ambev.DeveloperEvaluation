using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Profile for mapping between Product entity and GetProductResult.
/// This class is responsible for defining the mapping configuration
/// used by AutoMapper to transform Product and RatingValue entities
/// into their respective DTOs.
/// </summary>
public class GetProductProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductProfile"/> class.
    /// Configures the mappings for the GetProduct operation.
    /// </summary>
    public GetProductProfile()
    {
        // Maps Product entity to GetProductResult DTO.
        // Maps the Price property from the Amount value of the Price ValueObject.
        CreateMap<Product, GetProductResult>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount));

        // Maps RatingValue ValueObject to GetProductRatingResult DTO.
        CreateMap<RatingValue, GetProductRatingResult>();
    }
}
