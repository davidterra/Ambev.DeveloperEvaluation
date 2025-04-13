using Ambev.DeveloperEvaluation.Application.Carts.ListCarts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.ListCarts;

/// <summary>
/// Profile for mapping ListCarts feature requests, results, and commands.
/// </summary>
public class ListCartsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the ListCarts feature.
    /// Maps:
    /// - ListCartsRequest to ListCartsCommand
    /// - ListCartsResult to ListCartsResponse
    /// - ListCartsProductResult to ListCartsProductResponse
    /// </summary>
    public ListCartsProfile()
    {
        CreateMap<ListCartsRequest, ListCartsCommand>();
        CreateMap<ListCartsResult, ListCartsResponse>();
        CreateMap<ListCartsItemResult, ListCartsItemResponse>();
    }
}
