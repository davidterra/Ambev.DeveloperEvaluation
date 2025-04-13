using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
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
/// Contains unit tests for the <see cref="ListProductsHandler"/> class.
/// </summary>
public class ListProductsHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ListProductsHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListProductsHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public ListProductsHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Product, ListProductsResult>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount));
            cfg.CreateMap<RatingValue, ListProductsRatingResult>();
        });
        _mapper = mapperConfig.CreateMapper();

        _handler = new ListProductsHandler(_productRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid list products request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid request When listing products Then returns product list")]
    public async Task Handle_ValidRequest_ReturnsProductList()
    {
        // Given
        var command = new ListProductsCommand
        {
            Page = 1,
            Size = 10,
            OrderBy = "\"Title asc\"",
            Filters = null
        };

        var products = new List<Product>
        {
            ProductTestData.GenerateValidEntity(),
            ProductTestData.GenerateValidEntity(),
            ProductTestData.GenerateValidEntity(),
        }.AsQueryable();

        _productRepository.ListProducts(command.OrderBy, command.Filters).Returns(products);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_mapper.ProjectTo<ListProductsResult>(products));
        _productRepository.Received(1).ListProducts(command.OrderBy, command.Filters);
    }

    /// <summary>
    /// Tests that an invalid list products request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid request When listing products Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new ListProductsCommand
        {
            Page = 0, // Invalid page number
            Size = 10,
            OrderBy = "Title asc",
            Filters = null
        };

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }
}
