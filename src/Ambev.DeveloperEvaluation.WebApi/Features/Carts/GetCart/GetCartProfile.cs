
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// Profile for mapping GetCart feature requests, results, and responses.
/// </summary>
public class GetCartProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetCartProfile"/> class.
    /// Configures mappings for the GetCart feature.
    /// </summary>
    public GetCartProfile()
    {
        /// <summary>
        /// Maps an integer ID to a <see cref="GetCartCommand"/> object.
        /// </summary>
        CreateMap<int, GetCartCommand>()
            .ConstructUsing(id => new GetCartCommand(id));

        /// <summary>
        /// Maps a <see cref="GetCartResult"/> object to a <see cref="GetCartResponse"/> object.
        /// </summary>
        CreateMap<GetCartResult, GetCartResponse>();

        /// <summary>
        /// Maps a <see cref="GetCartItemResult"/> object to a <see cref="GetCartItemResponse"/> object.
        /// </summary>
        CreateMap<GetCartItemResult, GetCartItemResponse>();
    }
}
