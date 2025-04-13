using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Profile for mapping between User entity and UpdateUser operation results.
/// </summary>
public class UpdateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateUser operation.
    /// Maps commands to domain entities and value objects, and domain entities to result objects.
    /// </summary>
    public UpdateUserProfile()
    {
        // Maps UpdateUserCommand to User entity.
        CreateMap<UpdateUserCommand, User>();

        // Maps UpdateUserNameCommand to PersonNameValue object.
        CreateMap<UpdateUserNameCommand, PersonNameValue>();

        // Maps UpdateUserAddressCommand to AddressValue object.
        CreateMap<UpdateUserAddressCommand, AddressValue>();

        // Maps UpdateUserGeoLocationCommand to GeoLocationValue object.
        CreateMap<UpdateUserGeoLocationCommand, GeoLocationValue>();

        // Maps User entity to UpdateUserResult object.
        CreateMap<User, UpdateUserResult>();

        // Maps PersonNameValue object to UpdateUserNameResult object.
        CreateMap<PersonNameValue, UpdateUserNameResult>();

        // Maps AddressValue object to UpdateUserAddressResult object.
        CreateMap<AddressValue, UpdateUserAddressResult>();

        // Maps GeoLocationValue object to UpdateUserGeoLocationResult object.
        CreateMap<GeoLocationValue, UpdateUserGeoLocationResult>();
    }
}
