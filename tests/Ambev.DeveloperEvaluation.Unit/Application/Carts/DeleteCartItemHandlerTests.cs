using Ambev.DeveloperEvaluation.Application.Carts.DeleteCartItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentAssertions;
using FluentValidation;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

/// <summary>
/// Contains unit tests for the <see cref="DeleteCartItemHandler"/> class.
/// </summary>
public class DeleteCartItemHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly DeleteCartItemHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCartItemHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public DeleteCartItemHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _handler = new DeleteCartItemHandler(_cartRepository);
    }

    /// <summary>
    /// Tests that a valid delete cart item request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid request When deleting cart item Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = new DeleteCartItemCommand(1);

        var cartProduct = new CartItem
        {
            Id = command.Id,
            ProductId = 1,
            Quantity = 2,
            UnitPrice = new MonetaryValue(99.99m),
            CreatedAt = DateTime.UtcNow
        };

        _cartRepository.GetCartProductByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(cartProduct);
        _cartRepository.UpdateCartProductAsync(cartProduct, Arg.Any<CancellationToken>()).Returns(cartProduct);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        await _cartRepository.Received(1).GetCartProductByIdAsync(command.Id, Arg.Any<CancellationToken>());
        await _cartRepository.Received(1).UpdateCartProductAsync(cartProduct, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that deleting a non-existent cart item throws a resource not found exception.
    /// </summary>
    [Fact(DisplayName = "Given non-existent cart item When deleting cart item Then throws resource not found exception")]
    public async Task Handle_NonExistentCartItem_ThrowsResourceNotFoundException()
    {
        // Given
        var command = new DeleteCartItemCommand(1);

        _cartRepository.GetCartProductByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((CartItem?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage($"*Cart item with ID {command.Id} not found*");
    }

    /// <summary>
    /// Tests that an invalid delete cart item request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid request When deleting cart item Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new DeleteCartItemCommand(0); // Invalid ID

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }
}

