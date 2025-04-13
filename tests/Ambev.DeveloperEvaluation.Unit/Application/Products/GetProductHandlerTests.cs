using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
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
/// Contains unit tests for the <see cref="GetProductHandler"/> class.
/// </summary>
public class GetProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly GetProductHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public GetProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Product, GetProductResult>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(product => product.Price.Amount));
            cfg.CreateMap<RatingValue, GetProductRatingResult>();
        });
        _mapper = mapperConfig.CreateMapper();

        _handler = new GetProductHandler(_productRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid get product request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid request When retrieving product Then returns product details")]
    public async Task Handle_ValidRequest_ReturnsProductDetails()
    {
        // Given
        var command = new GetProductCommand(1);

        var product = ProductTestData.GenerateValidEntity();
        product.Id = command.Id;

        _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(product);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(new GetProductResult
        {
            Id = product.Id,
            Title = product.Title,
            Price = product.Price.Amount,
            Description = product.Description,
            Category = product.Category,
            Image = product.Image,
            Rating = new GetProductRatingResult
            {
                Rate = product.Rating.Rate,
                Count = product.Rating.Count
            },
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        });
        await _productRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that retrieving a non-existent product throws a resource not found exception.
    /// </summary>
    [Fact(DisplayName = "Given non-existent product When retrieving product Then throws resource not found exception")]
    public async Task Handle_NonExistentProduct_ThrowsResourceNotFoundException()
    {
        // Given
        var command = new GetProductCommand(1);

        _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((Product?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage($"*Product with ID {command.Id} not found*");
    }

    /// <summary>
    /// Tests that an invalid get product request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid request When retrieving product Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new GetProductCommand(0); // Invalid ID

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }
}
