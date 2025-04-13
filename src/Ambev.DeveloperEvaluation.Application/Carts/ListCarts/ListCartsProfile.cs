using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts;

/// <summary>
/// Profile class for mapping domain entities to DTOs for the ListCarts feature.
/// </summary>
public class ListCartsProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListCartsProfile"/> class.
    /// Configures mappings between <see cref="Cart"/> and <see cref="ListCartsResult"/>,
    /// as well as between <see cref="CartItem"/> and <see cref="ListCartsItemResult"/>.
    /// </summary>
    public ListCartsProfile()
    {
        // Mapping configuration for Cart to ListCartsResult
        CreateMap<Cart, ListCartsResult>();

        // Mapping configuration for CartProduct to ListCartsProductResult
        CreateMap<CartItem, ListCartsItemResult>()
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice.Amount))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount.Amount))
            .ForMember(dest => dest.DiscountPercent, opt => opt.MapFrom(src => src.DiscountPercent.Value));
    }
}
