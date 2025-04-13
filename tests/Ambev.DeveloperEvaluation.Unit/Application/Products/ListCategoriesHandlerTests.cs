using Ambev.DeveloperEvaluation.Application.Products.ListCategories;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

/// <summary>
/// Contains unit tests for the <see cref="ListCategoriesHandler"/> class.
/// </summary>
public class ListCategoriesHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly ListCategoriesHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListCategoriesHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public ListCategoriesHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _handler = new ListCategoriesHandler(_productRepository);
    }

    /// <summary>
    /// Tests that a valid list categories request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid request When listing categories Then returns category list")]
    public async Task Handle_ValidRequest_ReturnsCategoryList()
    {
        // Given
        var command = new ListCategoriesCommand();
        var categories = new List<string> { "Electronics", "Books", "Clothing" };

        _productRepository.ListCategoriesAsync(Arg.Any<CancellationToken>()).Returns(categories);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(new ListCategoriesResult { "Electronics", "Books", "Clothing" });
        await _productRepository.Received(1).ListCategoriesAsync(Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an empty category list is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given no categories When listing categories Then returns empty list")]
    public async Task Handle_NoCategories_ReturnsEmptyList()
    {
        // Given
        var command = new ListCategoriesCommand();
        var categories = new List<string>();

        _productRepository.ListCategoriesAsync(Arg.Any<CancellationToken>()).Returns(categories);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().BeEmpty();
        await _productRepository.Received(1).ListCategoriesAsync(Arg.Any<CancellationToken>());
    }
}
