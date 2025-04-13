using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;

/// <summary>
/// Profile for mapping DeleteProduct feature requests to commands.
/// This class is responsible for configuring the AutoMapper mappings
/// required to transform data from the API layer to the application layer
/// for the DeleteProduct feature.
/// </summary>
public class DeleteProductProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProductProfile"/> class.
    /// Configures the mapping between an integer (representing the product ID)
    /// and the <see cref="DeleteProductCommand"/> class.
    /// </summary>
    public DeleteProductProfile()
    {
        CreateMap<int, DeleteProductCommand>()
            .ConstructUsing(id => new DeleteProductCommand(id));
    }
}
