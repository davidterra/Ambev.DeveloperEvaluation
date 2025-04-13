using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

/// <summary>
/// Profile for mapping between Application and API UpdateUser requests and responses.
/// </summary>
public class UpdateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateUser feature.
    /// Maps requests to commands and results to responses.
    /// </summary>
    public UpdateUserProfile()
    {
        // Maps UpdateUserRequest to UpdateUserCommand for processing user updates.
        CreateMap<UpdateUserRequest, UpdateUserCommand>();

        // Maps UpdateUserNameRequest to UpdateUserNameCommand for processing name updates.
        CreateMap<UpdateUserNameRequest, UpdateUserNameCommand>();

        // Maps UpdateUserAddressRequest to UpdateUserAddressCommand for processing address updates.
        CreateMap<UpdateUserAddressRequest, UpdateUserAddressCommand>();

        // Maps UpdateUserGeoLocationRequest to UpdateUserGeoLocationCommand for processing geolocation updates.
        CreateMap<UpdateUserGeoLocationRequest, UpdateUserGeoLocationCommand>();

        // Maps UpdateUserResult to UpdateUserResponse for returning user update results.
        CreateMap<UpdateUserResult, UpdateUserResponse>();

        // Maps UpdateUserNameResult to UpdateUserNameResponse for returning name update results.
        CreateMap<UpdateUserNameResult, UpdateUserNameResponse>();

        // Maps UpdateUserAddressResult to UpdateUserAddressResponse for returning address update results.
        CreateMap<UpdateUserAddressResult, UpdateUserAddressResponse>();

        // Maps UpdateUserGeoLocationResult to UpdateUserGeoLocationResponse for returning geolocation update results.
        CreateMap<UpdateUserGeoLocationResult, UpdateUserGeoLocationResponse>();
    }
}
