using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;

/// <summary>
/// Profile for mapping DeleteCart feature requests to commands.
/// </summary>
public class DeleteCartProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCartProfile"/> class.
    /// Configures the mapping between an integer (cart ID) and the <see cref="DeleteCartCommand"/> object.
    /// </summary>
    public DeleteCartProfile()
    {
        CreateMap<int, DeleteCartCommand>()
            .ConstructUsing(id => new DeleteCartCommand(id));
    }
}
