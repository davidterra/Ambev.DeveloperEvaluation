using Ambev.DeveloperEvaluation.Application.Carts.CreateCartItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Products;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

/// <summary>
/// Contains unit tests for the <see cref="CreateCartItemHandler"/> class.
/// </summary>
public class CreateCartItemHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IDiscountService _discountService;
    private readonly IMapper _mapper;
    private readonly CreateCartItemHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartItemHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public CreateCartItemHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _cartRepository = Substitute.For<ICartRepository>();
        _discountService = Substitute.For<IDiscountService>();        

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateCartItemProductCommand, CartItem>();
            cfg.CreateMap<CartItem, CreateItemCartProductResult>()
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice.Amount))
                .ForMember(dest => dest.DiscountPercent, opt => opt.MapFrom(src => src.DiscountPercent.Value))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount.Amount));
        });

        _mapper = mapperConfig.CreateMapper();


        _handler = new CreateCartItemHandler(
            _productRepository,
            _userRepository,
            _cartRepository,
            _discountService,
            _mapper);
    }

    /// <summary>
    /// Tests that a valid create cart item request creates a new cart successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid request When creating new cart Then returns created cart item")]
    public async Task Handle_ValidRequest_CreatesNewCart()
    {
        // Given
        var command = new CreateCartItemCommand
        {
            UserId = Guid.NewGuid(),
            Product = new CreateCartItemProductCommand
            {
                ProductId = 1,
                Quantity = 2
            }
        };

        var user = new User { Id = command.UserId };
        var product = ProductTestData.GenerateValidEntity();

        var createdCart = new Cart
        {
            Id = 1,
            UserId = command.UserId,
            Items = new List<CartItem>
            {
                new CartItem
                {
                    ProductId = product.Id,
                    Quantity = command.Product.Quantity,
                    UnitPrice = product.Price
                }
            }
        };

        _userRepository.GetByIdAsync(command.UserId, Arg.Any<CancellationToken>()).Returns(user);
        _productRepository.GetByIdAsync(command.Product.ProductId, Arg.Any<CancellationToken>()).Returns(product);
        _cartRepository.GetActiveCartForUserAsync(command.UserId, Arg.Any<CancellationToken>()).Returns((Cart?)null);
        _cartRepository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>()).Returns(createdCart);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(new CreateCartItemResult
        {
            Id = createdCart.Id,
            UserId = createdCart.UserId,
            Product = _mapper.Map<CreateItemCartProductResult>(createdCart.Items.First())
        });
        await _cartRepository.Received(1).CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());        
    }

    /// <summary>
    /// Tests that creating a cart item with a non-existent user throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given non-existent user When creating cart item Then throws validation exception")]
    public async Task Handle_NonExistentUser_ThrowsValidationException()
    {
        // Given
        var command = new CreateCartItemCommand
        {
            UserId = Guid.NewGuid(),
            Product = new CreateCartItemProductCommand
            {
                ProductId = 1,
                Quantity = 2
            }
        };

        _userRepository.GetByIdAsync(command.UserId, Arg.Any<CancellationToken>()).Returns((User?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage($"*User with ID {command.UserId} does not exist*");
    }

    /// <summary>
    /// Tests that creating a cart item with a non-existent product throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given non-existent product When creating cart item Then throws validation exception")]
    public async Task Handle_NonExistentProduct_ThrowsValidationException()
    {
        // Given
        var command = new CreateCartItemCommand
        {
            UserId = Guid.NewGuid(),
            Product = new CreateCartItemProductCommand
            {
                ProductId = 1,
                Quantity = 2
            }
        };

        var user = new User { Id = command.UserId };

        _userRepository.GetByIdAsync(command.UserId, Arg.Any<CancellationToken>()).Returns(user);
        _productRepository.GetByIdAsync(command.Product.ProductId, Arg.Any<CancellationToken>()).Returns((Product?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage($"*Product with ID {command.Product.ProductId} does not exist*");
    }
}
