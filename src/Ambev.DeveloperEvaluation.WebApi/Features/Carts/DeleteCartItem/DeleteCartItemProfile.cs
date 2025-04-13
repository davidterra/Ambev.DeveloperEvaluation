using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCartItem;

/// <summary>
/// Profile for mapping DeleteCartItem feature requests to commands.
/// This class is responsible for defining the mapping configuration
/// between the source type (int) and the target type (DeleteCartCommand).
/// </summary>
public class DeleteCartItemProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCartItemProfile"/> class.
    /// Configures the mapping for the DeleteCartItem feature.
    /// </summary>
    public DeleteCartItemProfile()
    {
        /// <summary>
        /// Maps an integer (representing the cart item ID) to a <see cref="DeleteCartCommand"/>.
        /// The mapping uses a constructor to create a new instance of <see cref="DeleteCartCommand"/>
        /// with the provided ID.
        /// </summary>
        CreateMap<int, DeleteCartCommand>()
            .ConstructUsing(id => new DeleteCartCommand(id));
    }
}
