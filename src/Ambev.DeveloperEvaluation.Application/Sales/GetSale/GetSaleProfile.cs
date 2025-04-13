using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Profile for mapping between Sale and SaleItem entities to their respective result DTOs.
/// </summary>
public class GetSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleProfile"/> class.
    /// Configures mappings for Sale and SaleItem entities to their result DTOs.
    /// </summary>
    public GetSaleProfile()
    {
        /// <summary>
        /// Maps the <see cref="Sale"/> entity to the <see cref="GetSaleResult"/> DTO.
        /// Maps the TotalAmount property from the nested Amount object.
        /// </summary>
        CreateMap<Sale, GetSaleResult>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount.Amount));

        /// <summary>
        /// Maps the <see cref="SaleItem"/> entity to the <see cref="GetSaleItemResult"/> DTO.
        /// Maps the UnitPrice, DiscountPercent, and TotalAmount properties from their respective nested objects.
        /// </summary>
        CreateMap<SaleItem, GetSaleItemResult>()
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice.Amount))
            .ForMember(dest => dest.DiscountPercent, opt => opt.MapFrom(src => src.DiscountPercent.Value))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount.Amount));
    }
}
