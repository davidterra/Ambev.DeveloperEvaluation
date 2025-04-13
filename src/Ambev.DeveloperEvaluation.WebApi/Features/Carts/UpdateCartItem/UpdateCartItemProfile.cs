using Ambev.DeveloperEvaluation.Application.Carts.UpdateCartItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCartItem;

/// <summary>
/// Profile for mapping between Application and API UpdateCartItem requests and responses.
/// </summary>
public class UpdateCartItemProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the UpdateCartItem feature.
    /// Maps UpdateCartItemRequest to UpdateCartItemCommand and UpdateCartItemResult to UpdateCartItemResponse.
    /// </summary>
    public UpdateCartItemProfile()
    {
        CreateMap<UpdateCartItemRequest, UpdateCartItemCommand>();
        CreateMap<UpdateCartItemResult, UpdateCartItemResponse>();
    }
}
