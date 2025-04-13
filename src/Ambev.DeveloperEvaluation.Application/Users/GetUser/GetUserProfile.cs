using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Profile for mapping between User entity and GetUserResponse.
/// This class defines the mappings required to transform domain entities and value objects
/// into DTOs used in the GetUser operation.
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserProfile"/> class.
    /// Configures the mappings for the GetUser operation, including mappings for User, 
    /// PersonNameValue, AddressValue, and GeoLocationValue.
    /// </summary>
    public GetUserProfile()
    {
        /// <summary>
        /// Maps the <see cref="User"/> entity to the <see cref="GetUserResult"/> DTO.
        /// </summary>
        CreateMap<User, GetUserResult>();

        /// <summary>
        /// Maps the <see cref="PersonNameValue"/> value object to the <see cref="GetUserNameResult"/> DTO.
        /// </summary>
        CreateMap<PersonNameValue, GetUserNameResult>();

        /// <summary>
        /// Maps the <see cref="AddressValue"/> value object to the <see cref="GetUserAddressResult"/> DTO.
        /// </summary>
        CreateMap<AddressValue, GetUserAddressResult>();

        /// <summary>
        /// Maps the <see cref="GeoLocationValue"/> value object to the <see cref="GetUsersGeoLocationResult"/> DTO.
        /// </summary>
        CreateMap<GeoLocationValue, GetUsersGeoLocationResult>();
    }
}
