using Ambev.DeveloperEvaluation.Application.Products.ListCategory;
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
/// Contains unit tests for the <see cref="ListCategoryHandler"/> class.
/// </summary>
public class ListCategoryHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ListCategoryHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListCategoryHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public ListCategoryHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Product, ListCategoryResult>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount));
            cfg.CreateMap<RatingValue, ListCategoryRatingResult>();
        });
        _mapper = mapperConfig.CreateMapper();

        _handler = new ListCategoryHandler(_productRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid list category request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid request When listing category Then returns category products")]
    public async Task Handle_ValidRequest_ReturnsCategoryProducts()
    {
        // Given
        var command = new ListCategoryCommand("Electronics", "Title asc", null);

        var products = new List<Product>
        {
            ProductTestData.GenerateValidEntity(),
            ProductTestData.GenerateValidEntity(),
            ProductTestData.GenerateValidEntity(),
        }.AsQueryable();

        _productRepository.ListCategory(command.Category, command.OrderBy, command.Filters).Returns(products);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_mapper.ProjectTo<ListCategoryResult>(products));
        _productRepository.Received(1).ListCategory(command.Category, command.OrderBy, command.Filters);
    }

    /// <summary>
    /// Tests that an invalid list category request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid request When listing category Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new ListCategoryCommand("", "Title asc", null); // Invalid category

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }
}
