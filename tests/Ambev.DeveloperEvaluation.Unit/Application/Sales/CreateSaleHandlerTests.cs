using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="CreateSaleHandler"/> class.
/// </summary>
public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly ICartRepository _cartRepository;
    private readonly ISaleNumberGeneratorService _saleNumberGeneratorService;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _branchRepository = Substitute.For<IBranchRepository>();
        _cartRepository = Substitute.For<ICartRepository>();
        _saleNumberGeneratorService = Substitute.For<ISaleNumberGeneratorService>();
        _mediator = Substitute.For<IMediator>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleHandler(
            _saleRepository,
            _branchRepository,
            _cartRepository,
            _saleNumberGeneratorService,
            _mediator,
            _mapper);
    }

    /// <summary>
    /// Tests that a valid sale creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = new CreateSaleCommand(1, 1);
        var cart = new Cart
        {
            Id = command.CartId,
            UserId = Guid.NewGuid(),
            Items = new List<CartItem>
            {
                new CartItem { Id = 1, Quantity = 2, UnitPrice = new MonetaryValue(100) }
            }
        };
        var sale = new Sale
        {
            Id = 1,
            BranchId = command.BranchId,
            UserId = cart.UserId,
            Items = cart.Items.Select(i => new SaleItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };
        var result = new CreateSaleResult { Id = sale.Id };

        _cartRepository.GetByIdAsync(command.CartId, Arg.Any<CancellationToken>()).Returns(cart);
        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns(new Branch());
        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(sale);
        _mapper.Map<Sale>(cart).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(result);
        _saleNumberGeneratorService.Generate().Returns("SALE123");

        // When
        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSaleResult.Should().NotBeNull();
        createSaleResult.Id.Should().Be(sale.Id);
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await _cartRepository.Received(1).UpdateAsync(cart, Arg.Any<CancellationToken>());
        await _mediator.Received(1).Publish(Arg.Any<SaleCreatedEvent>());
    }

    /// <summary>
    /// Tests that an invalid sale creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateSaleCommand(0, 0); // Invalid command

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that a non-existent branch throws a resource not found exception.
    /// </summary>
    [Fact(DisplayName = "Given non-existent branch When creating sale Then throws resource not found exception")]
    public async Task Handle_NonExistentBranch_ThrowsResourceNotFoundException()
    {
        // Given
        var command = new CreateSaleCommand(1, 999); // Non-existent branch
        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns((Branch?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage("Branch with ID 999 not found.");
    }

    /// <summary>
    /// Tests that a non-existent cart throws a resource not found exception.
    /// </summary>
    [Fact(DisplayName = "Given non-existent cart When creating sale Then throws resource not found exception")]
    public async Task Handle_NonExistentCart_ThrowsResourceNotFoundException()
    {
        // Given
        var command = new CreateSaleCommand(999, 1); // Non-existent cart
        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns(new Branch() { Id = 1 });
        _cartRepository.GetByIdAsync(command.CartId, Arg.Any<CancellationToken>()).Returns((Cart?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage("*Cart with ID 999 not found*");
    }

    /// <summary>
    /// Tests that a canceled or empty cart throws an invalid operation exception.
    /// </summary>
    [Fact(DisplayName = "Given canceled or empty cart When creating sale Then throws invalid operation exception")]
    public async Task Handle_CanceledOrEmptyCart_ThrowsInvalidOperationException()
    {
        // Given
        var command = new CreateSaleCommand(1, 1);
        var cart = new Cart
        {
            Id = command.CartId,
            Status = CartStatus.Canceled,
            Items = new List<CartItem>()
        };

        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns(new Branch() { Id = 1});
        _cartRepository.GetByIdAsync(command.CartId, Arg.Any<CancellationToken>()).Returns(cart);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*Cart is cancelled or empty*");
    }
}
