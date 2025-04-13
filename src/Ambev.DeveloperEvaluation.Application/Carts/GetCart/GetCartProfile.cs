using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Profile class for mapping domain entities to DTOs related to the GetCart use case.
/// </summary>
public class GetCartProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetCartProfile"/> class.
    /// Configures mappings between <see cref="Cart"/> and <see cref="GetCartResult"/>,
    /// as well as between <see cref="CartItem"/> and <see cref="GetCartItemResult"/>.
    /// </summary>
    public GetCartProfile()
    {
        // Mapping configuration for Cart to GetCartResult
        CreateMap<Cart, GetCartResult>();

        // Mapping configuration for CartProduct to GetCartProductResult
        CreateMap<CartItem, GetCartItemResult>()
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice.Amount))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount.Amount))
            .ForMember(dest => dest.DiscountPercent, opt => opt.MapFrom(src => src.DiscountPercent.Value));
    }
}
