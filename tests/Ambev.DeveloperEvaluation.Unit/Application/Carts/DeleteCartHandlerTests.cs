using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

/// <summary>
/// Contains unit tests for the <see cref="DeleteCartHandler"/> class.
/// </summary>
public class DeleteCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly DeleteCartHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCartHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public DeleteCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _handler = new DeleteCartHandler(_cartRepository);
    }

    /// <summary>
    /// Tests that a valid delete cart request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid request When deleting cart Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = new DeleteCartCommand(1);

        var cart = new Cart
        {
            Id = command.Id,
            Status = CartStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        _cartRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(cart);
        _cartRepository.UpdateAsync(cart, Arg.Any<CancellationToken>()).Returns(cart);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        await _cartRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
        await _cartRepository.Received(1).UpdateAsync(cart, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that deleting a non-existent cart throws a resource not found exception.
    /// </summary>
    [Fact(DisplayName = "Given non-existent cart When deleting cart Then throws resource not found exception")]
    public async Task Handle_NonExistentCart_ThrowsResourceNotFoundException()
    {
        // Given
        var command = new DeleteCartCommand(1);

        _cartRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((Cart?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage($"*Cart with ID {command.Id} not found*");
    }

    /// <summary>
    /// Tests that an invalid delete cart request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid request When deleting cart Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new DeleteCartCommand(0); // Invalid ID

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }
}

