using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Profile for mapping between Product-related entities and DTOs used in the CreateProduct operation.
/// </summary>
public class CreateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the CreateProduct operation.
    /// Maps:
    /// - CreateProductCommand to Product entity.
    /// - CreateRatingCommand to RatingValue value object.
    /// - Product entity to CreateProductResult DTO, mapping the Price.Amount property.
    /// - RatingValue value object to CreateProductRatingResult DTO.
    /// </summary>
    public CreateProductProfile()
    {
        CreateMap<CreateProductCommand, Product>();
        CreateMap<CreateRatingCommand, RatingValue>();
        CreateMap<Product, CreateProductResult>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount));
        CreateMap<RatingValue, CreateProductRatingResult>();
    }
}
