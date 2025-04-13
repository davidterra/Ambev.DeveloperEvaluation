using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCartItem;

/// <summary>
/// Profile class for mapping between CreateCartItem-related commands, entities, and results.
/// </summary>
public class CreateCartItemProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartItemProfile"/> class.
    /// Configures mappings for CreateCartItemCommand, CreateCartItemProductCommand, Cart, and CartProduct.
    /// </summary>
    public CreateCartItemProfile()
    {
        // Maps CreateCartItemCommand to Cart entity.
        CreateMap<CreateCartItemCommand, Cart>();

        // Maps CreateCartItemProductCommand to CartProduct entity.
        CreateMap<CreateCartItemProductCommand, CartItem>();

        // Maps Cart entity to CreateCartItemResult.
        CreateMap<Cart, CreateCartItemResult>();

        // Maps CartProduct entity to CreateItemCartProductResult with custom property mappings.
        CreateMap<CartItem, CreateItemCartProductResult>()
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice.Amount))
            .ForMember(dest => dest.DiscountPercent, opt => opt.MapFrom(src => src.DiscountPercent.Value))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount.Amount));
    }
}
