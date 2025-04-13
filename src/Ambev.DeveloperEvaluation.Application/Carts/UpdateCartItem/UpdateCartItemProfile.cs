using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCartItem;

/// <summary>
/// Profile class for mapping between UpdateCartItemCommand, CartProduct, and UpdateCartItemResult.
/// </summary>
public class UpdateCartItemProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCartItemProfile"/> class.
    /// Configures the mappings for UpdateCartItemCommand to CartProduct and CartProduct to UpdateCartItemResult.
    /// </summary>
    public UpdateCartItemProfile()
    {
        // Maps UpdateCartItemCommand to CartProduct.
        CreateMap<UpdateCartItemCommand, CartItem>();

        // Maps CartProduct to UpdateCartItemResult with specific property mappings.
        CreateMap<CartItem, UpdateCartItemResult>()
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice.Amount))
            .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src => src.DiscountPercent.Value))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount.Amount));
    }
}
