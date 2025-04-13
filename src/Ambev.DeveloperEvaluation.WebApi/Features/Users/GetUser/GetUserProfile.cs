using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

/// <summary>
/// Profile for mapping GetUser feature requests to commands
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserProfile"/> class.
    /// Configures mappings between domain models and response DTOs for the GetUser feature.
    /// </summary>
    public GetUserProfile()
    {
        CreateMap<Guid, GetUserCommand>()
            .ConstructUsing(id => new GetUserCommand(id));
        // Maps ListUsersResult to GetUserResponse
        CreateMap<GetUserResult, GetUserResponse>();

        // Maps ListUsersNameResult to ListUsersNameResponse
        CreateMap<GetUserNameResult, GetUserNameResponse>();

        // Maps ListUsersAddressResult to ListUsersAddressResponse
        CreateMap<GetUserAddressResult, GetUserAddressResponse>();

        // Maps GetUsersGeoLocationResult to GetUsersGeoLocationResponse
        CreateMap<GetUsersGeoLocationResult, GetUserGeoLocationResponse>();
    }
}
