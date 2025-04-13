using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Users;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users;

/// <summary>
/// Contains unit tests for the <see cref="ListUsersHandler"/> class.
/// </summary>
public class ListUsersHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ListUsersHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListUsersHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public ListUsersHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, ListUsersResult>();
            cfg.CreateMap<PersonNameValue, ListUsersNameResult>();
            cfg.CreateMap<AddressValue, ListUsersAddressResult>();
            cfg.CreateMap<GeoLocationValue, ListUsersGeoLocationResult>();
        });
        _mapper = mapperConfig.CreateMapper();
        _handler = new ListUsersHandler(_userRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid list users request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid request When listing users Then returns user list")]
    public async Task Handle_ValidRequest_ReturnsUserList()
    {
        // Given
        var command = ListUsersHandlerTestData.GenerateValidCommand();
        var users = new List<User>
        {
            UserTestData.GenerateValidEntity(),
            UserTestData.GenerateValidEntity(),
            UserTestData.GenerateValidEntity(),
            UserTestData.GenerateValidEntity(),
            UserTestData.GenerateValidEntity(),
        }.AsQueryable();

        var expectedResult = users.Select(u => new ListUsersResult
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            Phone = u.Phone,
            Role = u.Role,
            Status = u.Status,
            Name = new ListUsersNameResult
            {
                FirstName = u.Name.FirstName,
                LastName = u.Name.LastName
            },
            Address = new ListUsersAddressResult
            {
                City = u.Address.City,
                State = u.Address.State,
                Street = u.Address.Street,
                Number = u.Address.Number,
                ZipCode = u.Address.ZipCode,
                GeoLocation = new ListUsersGeoLocationResult
                {
                    Latitude = u.Address.GeoLocation.Latitude,
                    Longitude = u.Address.GeoLocation.Longitude
                }
            }
        }).AsQueryable();

        _userRepository.ListUsers(command.OrderBy, command.Filters).Returns(users);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedResult);
        _userRepository.Received(1).ListUsers(command.OrderBy, command.Filters);
    }

    /// <summary>
    /// Tests that an invalid list users request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid request When listing users Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new ListUsersCommand() { OrderBy = "FirstName desc desc" }; // Invalid command

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }
}
