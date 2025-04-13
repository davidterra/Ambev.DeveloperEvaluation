using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="GetSaleHandler"/> class.
/// </summary>
public class GetSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly GetSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public GetSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSaleHandler(_saleRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid GetSaleCommand is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale ID When retrieving sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var saleId = 1;
        var sale = new Sale
        {
            Id = saleId,
            UserId = Guid.NewGuid(),
            BranchId = 1,
            Number = "SALE123",
            CreatedAt = DateTime.UtcNow,
            Items = new List<SaleItem>
            {
                new SaleItem { Id = 1, Quantity = 2, UnitPrice = new MonetaryValue(100) }
            }
        };
        var result = new GetSaleResult
        {
            Id = sale.Id,
            UserId = sale.UserId,
            BranchId = sale.BranchId,
            Number = sale.Number,
            CreatedAt = sale.CreatedAt,
            Items = sale.Items.Select(i => new GetSaleItemResult
            {
                Id = i.Id,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice.Amount
            }).ToList(),
            TotalAmount = sale.Items.Sum(i => i.TotalAmount.Amount)
        };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);
        _mapper.Map<GetSaleResult>(sale).Returns(result);

        // When
        var getSaleResult = await _handler.Handle(new GetSaleCommand(saleId), CancellationToken.None);

        // Then
        getSaleResult.Should().NotBeNull();
        getSaleResult.Id.Should().Be(sale.Id);
        getSaleResult.Number.Should().Be(sale.Number);
        await _saleRepository.Received(1).GetByIdAsync(saleId, Arg.Any<CancellationToken>());
        _mapper.Received(1).Map<GetSaleResult>(sale);
    }

    /// <summary>
    /// Tests that an invalid GetSaleCommand throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale ID When retrieving sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new GetSaleCommand(0); // Invalid ID

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that a non-existent sale throws a resource not found exception.
    /// </summary>
    [Fact(DisplayName = "Given non-existent sale ID When retrieving sale Then throws resource not found exception")]
    public async Task Handle_NonExistentSale_ThrowsResourceNotFoundException()
    {
        // Given
        var saleId = 999; // Non-existent sale
        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns((Sale?)null);

        // When
        var act = () => _handler.Handle(new GetSaleCommand(saleId), CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage("Sale with ID 999 not found.");
    }
}
