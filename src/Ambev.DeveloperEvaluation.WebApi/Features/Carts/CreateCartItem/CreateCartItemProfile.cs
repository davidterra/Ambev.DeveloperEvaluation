using Ambev.DeveloperEvaluation.Application.Carts.CreateCartItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCartItem;

/// <summary>
/// Profile for mapping between Application and API CreateCartItem feature.
/// </summary>
public class CreateCartItemProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartItemProfile"/> class.
    /// Configures mappings for CreateCartItem feature between API and Application layers.
    /// </summary>
    public CreateCartItemProfile()
    {
        /// <summary>
        /// Maps <see cref="CreateCartItemRequest"/> to <see cref="CreateCartItemCommand"/>.
        /// </summary>
        CreateMap<CreateCartItemRequest, CreateCartItemCommand>();

        /// <summary>
        /// Maps <see cref="CreateItemCartProductRequest"/> to <see cref="CreateCartItemProductCommand"/>.
        /// </summary>
        CreateMap<CreateItemCartProductRequest, CreateCartItemProductCommand>();

        /// <summary>
        /// Maps <see cref="CreateCartItemResult"/> to <see cref="CreateCartItemResponse"/>.
        /// </summary>
        CreateMap<CreateCartItemResult, CreateCartItemResponse>();

        /// <summary>
        /// Maps <see cref="CreateItemCartProductResult"/> to <see cref="CreateItemCartProductResponse"/>.
        /// </summary>
        CreateMap<CreateItemCartProductResult, CreateItemCartProductResponse>();
    }
}
