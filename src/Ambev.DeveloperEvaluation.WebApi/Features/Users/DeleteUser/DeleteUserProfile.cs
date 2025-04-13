using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;

/// <summary>
/// Profile for mapping DeleteUser feature requests to commands.
/// This class is responsible for configuring the mapping between a Guid and the DeleteUserCommand.
/// </summary>
public class DeleteUserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteUserProfile"/> class.
    /// Configures the mapping for the DeleteUser feature.
    /// </summary>
    public DeleteUserProfile()
    {
        // Maps a Guid to a DeleteUserCommand, constructing the command using the provided Guid.
        CreateMap<Guid, DeleteUserCommand>()
            .ConstructUsing(id => new DeleteUserCommand(id));
    }
}
