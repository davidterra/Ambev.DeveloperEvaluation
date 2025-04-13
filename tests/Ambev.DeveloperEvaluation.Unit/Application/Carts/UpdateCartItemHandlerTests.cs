using Ambev.DeveloperEvaluation.Application.Carts.UpdateCartItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

/// <summary>
/// Contains unit tests for the <see cref="UpdateCartItemHandler"/> class.
/// </summary>
public class UpdateCartItemHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IDiscountService _discountService;
    private readonly IMapper _mapper;
    private readonly UpdateCartItemHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCartItemHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public UpdateCartItemHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _discountService = Substitute.For<IDiscountService>();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CartItem, UpdateCartItemResult>()
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice.Amount))
                .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src => src.DiscountPercent.Value))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount.Amount));
        });
        _mapper = mapperConfig.CreateMapper();

        _handler = new UpdateCartItemHandler(_cartRepository, _discountService, _mapper);
    }

    /// <summary>
    /// Tests that a valid update cart item request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid request When updating cart item Then returns updated cart item")]
    public async Task Handle_ValidRequest_ReturnsUpdatedCartItem()
    {
        // Given
        var command = new UpdateCartItemCommand
        {
            Id = 1,
            Quantity = 3
        };

        var cartProduct = new CartItem
        {
            Id = command.Id,
            ProductId = 1,
            Quantity = 2,
            UnitPrice = new MonetaryValue(99.99m),
        };

        var updatedCartProduct = new CartItem
        {
            Id = command.Id,
            ProductId = 1,
            Quantity = command.Quantity,
            UnitPrice = cartProduct.UnitPrice,
        };

        _cartRepository.GetCartProductByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(cartProduct);
        _cartRepository.UpdateCartProductAsync(cartProduct, Arg.Any<CancellationToken>()).Returns(updatedCartProduct);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_mapper.Map<UpdateCartItemResult>(updatedCartProduct));
        await _cartRepository.Received(1).GetCartProductByIdAsync(command.Id, Arg.Any<CancellationToken>());
        await _cartRepository.Received(1).UpdateCartProductAsync(cartProduct, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that updating a non-existent cart item throws a resource not found exception.
    /// </summary>
    [Fact(DisplayName = "Given non-existent cart item When updating cart item Then throws resource not found exception")]
    public async Task Handle_NonExistentCartItem_ThrowsResourceNotFoundException()
    {
        // Given
        var command = new UpdateCartItemCommand
        {
            Id = 1,
            Quantity = 3
        };

        _cartRepository.GetCartProductByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((CartItem?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage($"*Cart product with ID {command.Id} not found*");
    }

    /// <summary>
    /// Tests that an invalid update cart item request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid request When updating cart item Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new UpdateCartItemCommand
        {
            Id = 0, // Invalid ID
            Quantity = 3
        };

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }
}


