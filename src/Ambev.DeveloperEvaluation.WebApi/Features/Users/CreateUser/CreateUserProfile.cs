using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Profile for mapping between Application and API CreateUser requests and responses.
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserProfile"/> class.
    /// Configures mappings for the CreateUser feature, including requests, commands, results, and responses.
    /// </summary>
    public CreateUserProfile()
    {
        /// <summary>
        /// Maps <see cref="CreateUserRequest"/> to <see cref="CreateUserCommand"/>.
        /// </summary>
        CreateMap<CreateUserRequest, CreateUserCommand>();

        /// <summary>
        /// Maps <see cref="CreateUserNameRequest"/> to <see cref="CreateUserNameCommand"/>.
        /// </summary>
        CreateMap<CreateUserNameRequest, CreateUserNameCommand>();

        /// <summary>
        /// Maps <see cref="CreateUserAddressRequest"/> to <see cref="CreateUserAddressCommand"/>.
        /// </summary>
        CreateMap<CreateUserAddressRequest, CreateUserAddressCommand>();

        /// <summary>
        /// Maps <see cref="CreateUserGeoLocationRequest"/> to <see cref="CreateUserGeoLocationCommand"/>.
        /// </summary>
        CreateMap<CreateUserGeoLocationRequest, CreateUserGeoLocationCommand>();

        /// <summary>
        /// Maps <see cref="CreateUserResult"/> to <see cref="CreateUserResponse"/>.
        /// </summary>
        CreateMap<CreateUserResult, CreateUserResponse>();

        /// <summary>
        /// Maps <see cref="CreateUserNameResult"/> to <see cref="CreateUserNameResponse"/>.
        /// </summary>
        CreateMap<CreateUserNameResult, CreateUserNameResponse>();

        /// <summary>
        /// Maps <see cref="CreateUserAddressResult"/> to <see cref="CreateUserAddressResponse"/>.
        /// </summary>
        CreateMap<CreateUserAddressResult, CreateUserAddressResponse>();

        /// <summary>
        /// Maps <see cref="CreateUserGeoLocationResult"/> to <see cref="CreateUserGeoLocationResponse"/>.
        /// </summary>
        CreateMap<CreateUserGeoLocationResult, CreateUserGeoLocationResponse>();
    }
}
