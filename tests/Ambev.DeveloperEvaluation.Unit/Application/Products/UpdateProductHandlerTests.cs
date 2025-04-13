using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
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
/// Contains unit tests for the <see cref="UpdateProductHandler"/> class.
/// </summary>
public class UpdateProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly UpdateProductHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProductHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public UpdateProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UpdateProductCommand, Product>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new MonetaryValue(src.Price)));
            cfg.CreateMap<UpdateRatingCommand, RatingValue>();
            cfg.CreateMap<Product, UpdateProductResult>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount));
            cfg.CreateMap<RatingValue, UpdateProductRatingResult>();
        });
        _mapper = mapperConfig.CreateMapper();

        _handler = new UpdateProductHandler(_productRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid update product request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid request When updating product Then returns updated product")]
    public async Task Handle_ValidRequest_ReturnsUpdatedProduct()
    {
        // Given
        var command = UpdateProductHandlerTestData.GenerateValidCommand();

        var existingProduct = ProductTestData.GenerateValidEntity();
        existingProduct.Id = command.Id;

        var updatedProduct = new Product
        {
            Id = command.Id,
            Title = command.Title,
            Price = new MonetaryValue(command.Price),
            Description = command.Description,
            Category = command.Category,
            Image = command.Image,
            Rating = new RatingValue(command.Rating.Rate, command.Rating.Count),
        };

        _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(existingProduct);
        _productRepository.UpdateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>()).Returns(updatedProduct);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(new UpdateProductResult
        {
            Id = updatedProduct.Id,
            Title = updatedProduct.Title,
            Price = updatedProduct.Price.Amount,
            Description = updatedProduct.Description,
            Category = updatedProduct.Category,
            Image = updatedProduct.Image,
            Rating = new UpdateProductRatingResult
            {
                Rate = updatedProduct.Rating.Rate,
                Count = updatedProduct.Rating.Count
            },
            CreatedAt = updatedProduct.CreatedAt,
            UpdatedAt = updatedProduct.UpdatedAt
        });
        await _productRepository.Received(1).UpdateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that updating a non-existent product throws a resource not found exception.
    /// </summary>
    [Fact(DisplayName = "Given non-existent product When updating product Then throws resource not found exception")]
    public async Task Handle_NonExistentProduct_ThrowsResourceNotFoundException()
    {
        // Given
        var command = new UpdateProductCommand
        {
            Id = 1,
            Title = "Updated Product",
            Price = 199.99m,
            Description = "Updated description",
            Category = "Electronics",
            Image = "https://example.com/image.jpg",
            Rating = new UpdateRatingCommand { Rate = 4.8m, Count = 15 }
        };

        _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((Product?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage($"*Product with ID {command.Id} not found*");
    }

    /// <summary>
    /// Tests that an invalid update product request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid request When updating product Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new UpdateProductCommand(); // Invalid command

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }
}

