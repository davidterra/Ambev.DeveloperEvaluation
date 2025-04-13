using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Products;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

/// <summary>
/// Contains unit tests for the <see cref="CreateProductHandler"/> class.
/// </summary>
public class CreateProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly CreateProductHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public CreateProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateProductCommand, Product>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new MonetaryValue(src.Price)));
            cfg.CreateMap<CreateRatingCommand, RatingValue>();
            cfg.CreateMap<Product, CreateProductResult>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount));
            cfg.CreateMap<RatingValue, CreateProductRatingResult>();
        });
        _mapper = mapperConfig.CreateMapper();

        _handler = new CreateProductHandler(_productRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid create product request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid request When creating product Then returns created product")]
    public async Task Handle_ValidRequest_ReturnsCreatedProduct()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateValidCommand();

        var product = new Product
        {
            Title = command.Title,
            Price = new MonetaryValue(command.Price),
            Description = command.Description,
            Category = command.Category,
            Image = command.Image,
            Rating = new RatingValue(command.Rating.Rate, command.Rating.Count)
        };

        _productRepository.GetByTitleAsync(command.Title, Arg.Any<CancellationToken>()).Returns((Product?)null);
        _productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>()).Returns(product);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(new CreateProductResult
        {
            Id = product.Id,
            Title = product.Title,
            Price = product.Price.Amount,
            Description = product.Description,
            Category = product.Category,
            Image = product.Image,
            Rating = new CreateProductRatingResult
            {
                Rate = product.Rating.Rate,
                Count = product.Rating.Count
            }
        });
        await _productRepository.Received(1).CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that creating a product with an existing title throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given existing product title When creating product Then throws validation exception")]
    public async Task Handle_ExistingProductTitle_ThrowsValidationException()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateValidCommand();

        var existingProduct = new Product { Id = 1, Title = command.Title };

        _productRepository.GetByTitleAsync(command.Title, Arg.Any<CancellationToken>()).Returns(existingProduct);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage($"*Product with title {command.Title} already exists*");
    }

    /// <summary>
    /// Tests that an invalid create product request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid request When creating product Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateProductCommand(); // Invalid command

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }
}
