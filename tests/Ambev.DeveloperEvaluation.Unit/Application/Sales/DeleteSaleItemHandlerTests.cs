using Ambev.DeveloperEvaluation.Application.Sales.DeleteSaleItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="DeleteSaleItemHandler"/> class.
/// </summary>
public class DeleteSaleItemHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMediator _mediator;
    private readonly DeleteSaleItemHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleItemHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public DeleteSaleItemHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mediator = Substitute.For<IMediator>();
        _handler = new DeleteSaleItemHandler(_saleRepository, _mediator);
    }

    /// <summary>
    /// Tests that a valid delete sale item request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale and item IDs When deleting sale item Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var saleId = 1;
        var itemId = 10;
        var sale = new Sale
        {
            Id = saleId,
            Items = new List<SaleItem>
            {
                new SaleItem { Id = itemId, Quantity = 2, UnitPrice = new MonetaryValue(100) }
            }
        };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);

        // When
        var response = await _handler.Handle(new DeleteSaleItemCommand(saleId, itemId), CancellationToken.None);

        // Then
        response.Should().NotBeNull();
        response.Success.Should().BeTrue();
        await _saleRepository.Received(1).UpdateItemAsync(Arg.Is<SaleItem>(i => i.Id == itemId && i.CanceledAt != null));
        await _saleRepository.Received(1).UpdateAsync(Arg.Is<Sale>(s => s.Id == saleId), Arg.Any<CancellationToken>());
        await _mediator.Received(1).Publish(Arg.Is<SaleItemCancelledEvent>(e => e.SaleItemId == itemId), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid delete sale item request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale or item IDs When deleting sale item Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new DeleteSaleItemCommand(0, 0); // Invalid IDs

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that a non-existent sale throws a resource not found exception.
    /// </summary>
    [Fact(DisplayName = "Given non-existent sale ID When deleting sale item Then throws resource not found exception")]
    public async Task Handle_NonExistentSale_ThrowsResourceNotFoundException()
    {
        // Given
        var saleId = 999; // Non-existent sale
        var itemId = 10;
        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns((Sale?)null);

        // When
        var act = () => _handler.Handle(new DeleteSaleItemCommand(saleId, itemId), CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage("Sale with ID 999 not found");
    }

    /// <summary>
    /// Tests that a non-existent sale item throws a resource not found exception.
    /// </summary>
    [Fact(DisplayName = "Given non-existent sale item ID When deleting sale item Then throws resource not found exception")]
    public async Task Handle_NonExistentSaleItem_ThrowsResourceNotFoundException()
    {
        // Given
        var saleId = 1;
        var itemId = 999; // Non-existent item
        var sale = new Sale
        {
            Id = saleId,
            Items = new List<SaleItem>()
        };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);

        // When
        var act = () => _handler.Handle(new DeleteSaleItemCommand(saleId, itemId), CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage("Sale item with ID 999 not found");
    }

    /// <summary>
    /// Tests that a canceled sale item is updated correctly in the repository.
    /// </summary>
    [Fact(DisplayName = "Given valid sale and item IDs When deleting sale item Then marks item as canceled")]
    public async Task Handle_ValidRequest_MarksItemAsCanceled()
    {
        // Given
        var saleId = 1;
        var itemId = 10;
        var sale = new Sale
        {
            Id = saleId,
            Items = new List<SaleItem>
            {
                new SaleItem { Id = itemId, Quantity = 2, UnitPrice = new MonetaryValue(100) }
            }
        };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);

        // When
        await _handler.Handle(new DeleteSaleItemCommand(saleId, itemId), CancellationToken.None);

        // Then
        await _saleRepository.Received(1).UpdateItemAsync(Arg.Is<SaleItem>(i => i.Id == itemId && i.CanceledAt != null));
    }

    /// <summary>
    /// Tests that the SaleItemCancelledEvent is published after a sale item is deleted.
    /// </summary>
    [Fact(DisplayName = "Given valid sale and item IDs When deleting sale item Then publishes SaleItemCancelledEvent")]
    public async Task Handle_ValidRequest_PublishesSaleItemCancelledEvent()
    {
        // Given
        var saleId = 1;
        var itemId = 10;
        var sale = new Sale
        {
            Id = saleId,
            Items = new List<SaleItem>
            {
                new SaleItem { Id = itemId, Quantity = 2, UnitPrice = new MonetaryValue(100) }
            }
        };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);

        // When
        await _handler.Handle(new DeleteSaleItemCommand(saleId, itemId), CancellationToken.None);

        // Then
        await _mediator.Received(1).Publish(Arg.Is<SaleItemCancelledEvent>(e => e.SaleItemId == itemId), Arg.Any<CancellationToken>());
    }
}
