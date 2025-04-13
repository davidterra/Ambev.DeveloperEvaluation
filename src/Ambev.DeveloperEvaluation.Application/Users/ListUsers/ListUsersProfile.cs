using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

/// <summary>
/// Profile for mapping between User entity and ListUsersResult, including related value objects.
/// </summary>
public class ListUsersProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the ListUsers operation.
    /// Maps User entity to ListUsersResult and its related value objects to their respective result types.
    /// </summary>
    public ListUsersProfile()
    {
        // Maps User entity to ListUsersResult DTO
        CreateMap<User, ListUsersResult>();

        // Maps PersonNameValue value object to ListUsersNameResult DTO
        CreateMap<PersonNameValue, ListUsersNameResult>();

        // Maps AddressValue value object to ListUsersAddressResult DTO
        CreateMap<AddressValue, ListUsersAddressResult>();

        // Maps GeoLocationValue value object to ListUsersGeoLocationResult DTO
        CreateMap<GeoLocationValue, ListUsersGeoLocationResult>();
    }
}
