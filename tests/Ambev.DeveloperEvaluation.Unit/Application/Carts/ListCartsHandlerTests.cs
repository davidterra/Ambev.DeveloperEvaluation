using Ambev.DeveloperEvaluation.Application.Carts.ListCarts;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

/// <summary>
/// Contains unit tests for the <see cref="ListCartsHandler"/> class.
/// </summary>
public class ListCartsHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly ListCartsHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListCartsHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public ListCartsHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Cart, ListCartsResult>();
            cfg.CreateMap<CartItem, ListCartsItemResult>()
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice.Amount))
                .ForMember(dest => dest.DiscountPercent, opt => opt.MapFrom(src => src.DiscountPercent.Value))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount.Amount));
        });
        _mapper = mapperConfig.CreateMapper();

        _handler = new ListCartsHandler(_cartRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid list carts request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid request When listing carts Then returns cart list")]
    public async Task Handle_ValidRequest_ReturnsCartList()
    {
        // Given
        var command = new ListCartsCommand
        {
            Page = 1,
            Size = 10,
            OrderBy = "\"CreatedAt desc\"",
            Filters = null
        };

        var carts = new List<Cart>
        {
            new Cart
            {
                Id = 1,
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
            },
            new Cart
            {
                Id = 2,
                UserId = Guid.NewGuid(),
                Status = CartStatus.Converted,
                Items = new List<CartItem>()
                {
                    new CartItem
                    {
                        Id = 2,
                        ProductId = 1,
                        Quantity = 2,
                        UnitPrice = new MonetaryValue(89.99m),
                    }
                },
            }
        }.AsQueryable();

        _cartRepository.ListCarts(command.OrderBy, command.Filters).Returns(carts);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_mapper.ProjectTo<ListCartsResult>(carts));
        _cartRepository.Received(1).ListCarts(command.OrderBy, command.Filters);
    }

    /// <summary>
    /// Tests that an invalid list carts request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid request When listing carts Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new ListCartsCommand
        {
            Page = 0, // Invalid page number
            Size = 10,
            OrderBy = "\"CreatedAt desc\"",
            Filters = null
        };

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }
}


