using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Profile for mapping between User entity and CreateUserResponse
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser operation
    /// </summary>
    public CreateUserProfile()
    {
        CreateMap<CreateUserCommand, User>();
        CreateMap<CreateUserNameCommand, PersonNameValue>();
        CreateMap<CreateUserAddressCommand, AddressValue>();
        CreateMap<CreateUserGeoLocationCommand, GeoLocationValue>();
        CreateMap<User, CreateUserResult>();
        CreateMap<PersonNameValue, CreateUserNameResult>();
        CreateMap<AddressValue, CreateUserAddressResult>();
        CreateMap<GeoLocationValue, CreateUserGeoLocationResult>();
    }
}
