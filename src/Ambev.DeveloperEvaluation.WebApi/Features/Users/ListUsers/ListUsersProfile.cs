using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;

/// <summary>
/// Profile for mapping ListUsers feature requests, results, and related objects to their corresponding commands and responses.
/// </summary>
public class ListUsersProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the ListUsers feature.
    /// Maps requests to commands, results to responses, and nested result objects to their respective response objects.
    /// </summary>
    public ListUsersProfile()
    {
        // Maps a ListUsersRequest object to a ListUsersCommand object.
        CreateMap<ListUsersRequest, ListUsersCommand>();

        // Maps a ListUsersResult object to a ListUsersResponse object.
        CreateMap<ListUsersResult, ListUsersResponse>();

        // Maps a ListUsersNameResult object to a ListUsersNameResponse object.
        CreateMap<ListUsersNameResult, ListUsersNameResponse>();

        // Maps a ListUsersAddressResult object to a ListUsersAddressResponse object.
        CreateMap<ListUsersAddressResult, ListUsersAddressResponse>();

        // Maps a ListUsersGeoLocationResult object to a ListUsersGeoLocationResponse object.
        CreateMap<ListUsersGeoLocationResult, ListUsersGeoLocationResponse>();
    }
}
