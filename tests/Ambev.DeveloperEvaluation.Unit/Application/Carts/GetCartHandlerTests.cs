using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

/// <summary>
/// Contains unit tests for the <see cref="GetCartHandler"/> class.
/// </summary>
public class GetCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly GetCartHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCartHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public GetCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Cart, GetCartResult>();
            cfg.CreateMap<CartItem, GetCartItemResult>()
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice.Amount))
                .ForMember(dest => dest.DiscountPercent, opt => opt.MapFrom(src => src.DiscountPercent.Value))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount.Amount));
        });
        _mapper = mapperConfig.CreateMapper();

        _handler = new GetCartHandler(_cartRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid get cart request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid request When retrieving cart Then returns cart details")]
    public async Task Handle_ValidRequest_ReturnsCartDetails()
    {
        // Given
        var command = new GetCartCommand(1);

        var cart = new Cart
        {
            Id = command.Id,
            UserId = Guid.NewGuid(),
            Status = CartStatus.Active,
            CreatedAt = DateTime.UtcNow,
            Items = new List<CartItem>
            {
                new CartItem
                {
                    Id = 1,
                    ProductId = 1,
                    Quantity = 2,
                    UnitPrice = new MonetaryValue(99.99m),
                }
            },
        };

        _cartRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(cart);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_mapper.Map<GetCartResult>(cart));
        await _cartRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that retrieving a non-existent cart throws a resource not found exception.
    /// </summary>
    [Fact(DisplayName = "Given non-existent cart When retrieving cart Then throws resource not found exception")]
    public async Task Handle_NonExistentCart_ThrowsResourceNotFoundException()
    {
        // Given
        var command = new GetCartCommand(1);

        _cartRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((Cart?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage($"*Cart with ID {command.Id} not found*");
    }

    /// <summary>
    /// Tests that an invalid get cart request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid request When retrieving cart Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new GetCartCommand(0); // Invalid ID

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }
}


