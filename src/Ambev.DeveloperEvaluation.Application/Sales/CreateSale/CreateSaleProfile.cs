using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Profile for mapping between domain entities and DTOs for the CreateSale operation.
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleProfile"/> class.
    /// Configures mappings for the CreateSale operation, including mappings between
    /// Sale, SaleItem, Cart, and their respective DTOs.
    /// </summary>
    public CreateSaleProfile()
    {
        /// <summary>
        /// Maps the <see cref="Sale"/> entity to the <see cref="CreateSaleResult"/> DTO.
        /// Maps the TotalAmount property from the nested Amount value.
        /// </summary>
        CreateMap<Sale, CreateSaleResult>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount.Amount));

        /// <summary>
        /// Maps the <see cref="SaleItem"/> entity to the <see cref="CreateSaleItemResult"/> DTO.
        /// Maps UnitPrice, DiscountPercent, and TotalAmount properties from their respective nested values.
        /// </summary>
        CreateMap<SaleItem, CreateSaleItemResult>()
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice.Amount))
            .ForMember(dest => dest.DiscountPercent, opt => opt.MapFrom(src => src.DiscountPercent.Value))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount.Amount));

        /// <summary>
        /// Maps the <see cref="Cart"/> entity to the <see cref="Sale"/> entity.
        /// Ignores the Id property during mapping.
        /// </summary>
        CreateMap<Cart, Sale>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        /// <summary>
        /// Maps the <see cref="CartItem"/> entity to the <see cref="SaleItem"/> entity.
        /// Ignores the Id property during mapping.
        /// </summary>
        CreateMap<CartItem, SaleItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
