using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="UpdateSaleHandler"/> class.
/// </summary>
public class UpdateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly UpdateSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public UpdateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _branchRepository = Substitute.For<IBranchRepository>();
        _mediator = Substitute.For<IMediator>();
        _mapper = Substitute.For<IMapper>();
        _handler = new UpdateSaleHandler(_saleRepository, _branchRepository, _mediator, _mapper);
    }

    /// <summary>
    /// Tests that a valid update sale request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When updating sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = new UpdateSaleCommand { Id = 1, BranchId = 2 };
        var sale = new Sale
        {
            Id = command.Id,
            BranchId = 1,
            UpdatedAt = null
        };
        var updatedSale = new Sale
        {
            Id = command.Id,
            BranchId = command.BranchId,
            UpdatedAt = DateTime.UtcNow
        };
        var result = new UpdateSaleResult
        {
            Id = updatedSale.Id,
            BranchId = updatedSale.BranchId,
            Number = "SALE123",
            CreatedAt = updatedSale.CreatedAt,
            TotalAmount = updatedSale.TotalAmount.Amount
        };

        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns(new Branch());
        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(sale);
        _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(updatedSale);
        _mapper.Map<UpdateSaleResult>(updatedSale).Returns(result);

        // When
        var updateSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        updateSaleResult.Should().NotBeNull();
        updateSaleResult.Id.Should().Be(updatedSale.Id);
        updateSaleResult.BranchId.Should().Be(updatedSale.BranchId);
        await _branchRepository.Received(1).GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>());
        await _saleRepository.Received(1).UpdateAsync(Arg.Is<Sale>(s => s.BranchId == command.BranchId), Arg.Any<CancellationToken>());
        await _mediator.Received(1).Publish(Arg.Is<SaleModifiedEvent>(e => e.SaleId == updatedSale.Id), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid update sale request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale data When updating sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new UpdateSaleCommand(); // Missing required fields

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that a non-existent branch throws a resource not found exception.
    /// </summary>
    [Fact(DisplayName = "Given non-existent branch ID When updating sale Then throws resource not found exception")]
    public async Task Handle_NonExistentBranch_ThrowsResourceNotFoundException()
    {
        // Given
        var command = new UpdateSaleCommand { Id = 1, BranchId = 999 }; // Non-existent branch
        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns((Branch?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage("Branch with ID 999 not found.");
    }

    /// <summary>
    /// Tests that a non-existent sale throws a resource not found exception.
    /// </summary>
    [Fact(DisplayName = "Given non-existent sale ID When updating sale Then throws resource not found exception")]
    public async Task Handle_NonExistentSale_ThrowsResourceNotFoundException()
    {
        // Given
        var command = new UpdateSaleCommand { Id = 999, BranchId = 1 }; // Non-existent sale
        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns(new Branch());
        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((Sale?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage("Sale with ID 999 not found.");
    }

    /// <summary>
    /// Tests that the SaleModifiedEvent is published after a sale is updated.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When updating sale Then publishes SaleModifiedEvent")]
    public async Task Handle_ValidRequest_PublishesSaleModifiedEvent()
    {
        // Given
        var command = new UpdateSaleCommand { Id = 1, BranchId = 2 };
        var sale = new Sale
        {
            Id = command.Id,
            BranchId = 1
        };
        var updatedSale = new Sale
        {
            Id = command.Id,
            BranchId = command.BranchId
        };

        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns(new Branch());
        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(sale);
        _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(updatedSale);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _mediator.Received(1).Publish(Arg.Is<SaleModifiedEvent>(e => e.SaleId == updatedSale.Id), Arg.Any<CancellationToken>());
    }
}
