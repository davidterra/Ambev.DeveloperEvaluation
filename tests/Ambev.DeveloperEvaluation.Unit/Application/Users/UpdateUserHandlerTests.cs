using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
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
/// Contains unit tests for the <see cref="UpdateUserHandler"/> class.
/// </summary>
public class UpdateUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly UpdateUserHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public UpdateUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordHasher = Substitute.For<IPasswordHasher>();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UpdateUserCommand, User>();
            cfg.CreateMap<UpdateUserNameCommand, PersonNameValue>();
            cfg.CreateMap<UpdateUserAddressCommand, AddressValue>();
            cfg.CreateMap<UpdateUserGeoLocationCommand, GeoLocationValue>();
            cfg.CreateMap<User, UpdateUserResult>();
            cfg.CreateMap<PersonNameValue, UpdateUserNameResult>();
            cfg.CreateMap<AddressValue, UpdateUserAddressResult>();
            cfg.CreateMap<GeoLocationValue, UpdateUserGeoLocationResult>();
        });
        _mapper = mapperConfig.CreateMapper();

        _handler = new UpdateUserHandler(_userRepository, _mapper, _passwordHasher);
    }

    /// <summary>
    /// Tests that a valid update user request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid request When updating user Then returns updated user")]
    public async Task Handle_ValidRequest_ReturnsUpdatedUser()
    {
        // Given
        var command = UpdateUserHandlerTestData.GenerateValidCommand();

        var existingUser = UserTestData.GenerateValidEntity();

        var updatedUser = UserTestData.GenerateValidEntity();

        existingUser.Id = command.Id;
        updatedUser.Id = command.Id;
        updatedUser.Username = command.Username;
        updatedUser.Email = command.Email;
        updatedUser.Phone = command.Phone;
        updatedUser.Role = command.Role;
        updatedUser.Status = command.Status;

        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns(existingUser);
        _userRepository.UpdateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>()).Returns(updatedUser);
        _passwordHasher.VerifyPassword(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(new UpdateUserResult
        {
            Id = updatedUser.Id,
            Username = updatedUser.Username,
            Email = updatedUser.Email,
            Phone = updatedUser.Phone,
            Role = updatedUser.Role,
            Status = updatedUser.Status,
            Password = updatedUser.Password,
            Name = new UpdateUserNameResult
            {
                FirstName = updatedUser.Name.FirstName,
                LastName = updatedUser.Name.LastName
            },
            Address = new UpdateUserAddressResult
            {
                City = updatedUser.Address.City,
                State = updatedUser.Address.State,
                Street = updatedUser.Address.Street,
                Number = updatedUser.Address.Number,
                ZipCode = updatedUser.Address.ZipCode,
                GeoLocation = new UpdateUserGeoLocationResult
                {
                    Latitude = updatedUser.Address.GeoLocation.Latitude,
                    Longitude = updatedUser.Address.GeoLocation.Longitude
                }
            }

        });

        await _userRepository.Received(1).UpdateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid update user request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid request When updating user Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new UpdateUserCommand(); // Invalid command

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that updating a non-existent user throws a resource not found exception.
    /// </summary>
    [Fact(DisplayName = "Given non-existent user When updating user Then throws resource not found exception")]
    public async Task Handle_NonExistentUser_ThrowsResourceNotFoundException()
    {
        // Given
        var command = UpdateUserHandlerTestData.GenerateValidCommand();

        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns((User?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage($"User with email {command.Email} does not exist");
    }

    /// <summary>
    /// Tests that providing an incorrect password throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given incorrect password When updating user Then throws validation exception")]
    public async Task Handle_IncorrectPassword_ThrowsValidationException()
    {
        // Given
        var command = UpdateUserHandlerTestData.GenerateValidCommand();

        var existingUser = new User
        {
            Id = command.Id,
            Email = command.Email,
            Password = _passwordHasher.HashPassword("CorrectPassword123!")
        };

        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns(existingUser);
        _passwordHasher.VerifyPassword(command.Password, existingUser.Password).Returns(false);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage("*The provided password is incorrect*");
    }
}
