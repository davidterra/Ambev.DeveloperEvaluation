using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="DeleteSaleHandler"/> class.
/// </summary>
public class DeleteSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMediator _mediator;
    private readonly DeleteSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public DeleteSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mediator = Substitute.For<IMediator>();
        _handler = new DeleteSaleHandler(_saleRepository, _mediator);
    }

    /// <summary>
    /// Tests that a valid delete sale request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale ID When deleting sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var saleId = 1;
        var sale = new Sale { Id = saleId };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);

        // When
        var response = await _handler.Handle(new DeleteSaleCommand(saleId), CancellationToken.None);

        // Then
        response.Should().NotBeNull();
        response.Success.Should().BeTrue();
        await _saleRepository.Received(1).UpdateAsync(Arg.Is<Sale>(s => s.Id == saleId && s.CanceledAt != null), Arg.Any<CancellationToken>());
        await _mediator.Received(1).Publish(Arg.Is<SaleCancelledEvent>(e => e.SaleId == saleId), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid delete sale request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale ID When deleting sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new DeleteSaleCommand(0); // Missing required ID

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that a non-existent sale throws a resource not found exception.
    /// </summary>
    [Fact(DisplayName = "Given non-existent sale ID When deleting sale Then throws resource not found exception")]
    public async Task Handle_NonExistentSale_ThrowsResourceNotFoundException()
    {
        // Given
        var saleId = 999; // Non-existent sale
        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns((Sale?)null);

        // When
        var act = () => _handler.Handle(new DeleteSaleCommand(saleId), CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage("Sale with ID 999 not found");
    }

    /// <summary>
    /// Tests that a canceled sale is updated correctly in the repository.
    /// </summary>
    [Fact(DisplayName = "Given valid sale ID When deleting sale Then marks sale as canceled")]
    public async Task Handle_ValidRequest_MarksSaleAsCanceled()
    {
        // Given
        var saleId = 1;
        var sale = new Sale { Id = saleId };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);

        // When
        await _handler.Handle(new DeleteSaleCommand(saleId), CancellationToken.None);

        // Then
        await _saleRepository.Received(1).UpdateAsync(Arg.Is<Sale>(s => s.Id == saleId && s.CanceledAt != null), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that the SaleCancelledEvent is published after a sale is deleted.
    /// </summary>
    [Fact(DisplayName = "Given valid sale ID When deleting sale Then publishes SaleCancelledEvent")]
    public async Task Handle_ValidRequest_PublishesSaleCancelledEvent()
    {
        // Given
        var saleId = 1;
        var sale = new Sale { Id = saleId };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);

        // When
        await _handler.Handle(new DeleteSaleCommand(saleId), CancellationToken.None);

        // Then
        await _mediator.Received(1).Publish(Arg.Is<SaleCancelledEvent>(e => e.SaleId == saleId), Arg.Any<CancellationToken>());
    }
}
