using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Users;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users;

/// <summary>
/// Contains unit tests for the <see cref="GetUserHandler"/> class.
/// </summary>
public class GetUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly GetUserHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public GetUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetUserHandler(_userRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid GetUserCommand request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid user ID When retrieving user Then returns user details")]
    public async Task Handle_ValidRequest_ReturnsUserDetails()
    {
        // Given
        var user = UserTestData.GenerateValidEntity();
        var userId = user.Id;
        var command = new GetUserCommand(userId);
        var result = new GetUserResult
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Phone = user.Phone,
            Role = user.Role,
            Status = user.Status,
            Name = new GetUserNameResult
            {
                LastName = user.Name.LastName,
                FirstName = user.Name.FirstName
            },
            Address = new GetUserAddressResult
            {
                City = user.Address.City,
                State = user.Address.State,
                Street = user.Address.Street,
                Number = user.Address.Number,
                ZipCode = user.Address.ZipCode,
                GeoLocation = new GetUsersGeoLocationResult
                {
                    Latitude = user.Address.GeoLocation.Latitude,
                    Longitude = user.Address.GeoLocation.Longitude
                }
            }
        };

        _userRepository.GetByIdAsync(userId, Arg.Any<CancellationToken>())
            .Returns(user);
        _mapper.Map<GetUserResult>(user).Returns(result);

        // When
        var response = await _handler.Handle(command, CancellationToken.None);

        // Then
        response.Should().NotBeNull();
        response.Id.Should().Be(user.Id);
        response.Username.Should().Be(user.Username);
        response.Email.Should().Be(user.Email);
        await _userRepository.Received(1).GetByIdAsync(userId, Arg.Any<CancellationToken>());
        _mapper.Received(1).Map<GetUserResult>(user);
    }

    /// <summary>
    /// Tests that an invalid GetUserCommand request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid user ID When retrieving user Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new GetUserCommand(Guid.Empty); // Invalid ID

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that a GetUserCommand request for a non-existent user throws a KeyNotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given non-existent user ID When retrieving user Then throws key not found exception")]
    public async Task Handle_NonExistentUser_ThrowsKeyNotFoundException()
    {
        // Given
        var userId = Guid.NewGuid();
        var command = new GetUserCommand(userId);

        _userRepository.GetByIdAsync(userId, Arg.Any<CancellationToken>())
            .Returns((User?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"User with ID {userId} not found");
    }
}
