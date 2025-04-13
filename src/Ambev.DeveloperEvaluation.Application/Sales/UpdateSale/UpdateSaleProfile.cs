using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Profile for mapping entities related to the UpdateSale operation.
/// This class defines mappings between domain entities and DTOs used in the UpdateSale process.
/// </summary>
public class UpdateSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleProfile"/> class.
    /// Configures mappings for the UpdateSale operation, including mappings for Sale, SaleItem, Cart, and CartItem entities.
    /// </summary>
    public UpdateSaleProfile()
    {
        // Maps Sale entity to UpdateSaleResult DTO, mapping TotalAmount property.
        CreateMap<Sale, UpdateSaleResult>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount.Amount));

        // Maps SaleItem entity to UpdateSaleItemResult DTO, mapping UnitPrice, DiscountPercent, and TotalAmount properties.
        CreateMap<SaleItem, UpdateSaleItemResult>()
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice.Amount))
            .ForMember(dest => dest.DiscountPercent, opt => opt.MapFrom(src => src.DiscountPercent.Value))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount.Amount));

        // Maps Cart entity to Sale entity, ignoring the Id property during mapping.
        CreateMap<Cart, Sale>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        // Maps CartItem entity to SaleItem entity, ignoring the Id property during mapping.
        CreateMap<CartItem, SaleItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
