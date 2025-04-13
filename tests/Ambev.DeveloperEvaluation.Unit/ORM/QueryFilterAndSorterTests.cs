using Ambev.DeveloperEvaluation.ORM.Query;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.ORM;

/// <summary>
/// Contains unit tests for the <see cref="QueryFilterAndSorter{TEntity}"/> class.
/// </summary>
public class QueryFilterAndSorterTests
{
    private readonly IQueryable<TestEntity> _testData;

    /// <summary>
    /// Initializes a new instance of the <see cref="QueryFilterAndSorterTests"/> class.
    /// </summary>
    public QueryFilterAndSorterTests()
    {
        _testData = new List<TestEntity>
        {
            new TestEntity { Id = 1, Name = "Alice", Age = 25 },
            new TestEntity { Id = 2, Name = "Bob", Age = 30 },
            new TestEntity { Id = 3, Name = "Charlie", Age = 35 },
            new TestEntity { Id = 4, Name = "Alice", Age = 40 }
        }.AsQueryable();
    }

    /// <summary>
    /// Tests that filters are applied correctly.
    /// </summary>
    [Fact(DisplayName = "Given valid filters When applied Then returns filtered results")]
    public void Apply_WithValidFilters_ReturnsFilteredResults()
    {
        // Given
        var filters = new Dictionary<string, string>
        {
            { "Name", "Alice" },
            { "_minAge", "30" }
        };
        var sorter = new QueryFilterAndSorter<TestEntity>(_testData, string.Empty, filters);

        // When
        var result = sorter.Apply().ToList();

        // Then
        Assert.Single(result);
        Assert.Equal(4, result.First().Id);
    }

    /// <summary>
    /// Tests that ordering is applied correctly.
    /// </summary>
    [Fact(DisplayName = "Given valid ordering When applied Then returns ordered results")]
    public void Apply_WithValidOrdering_ReturnsOrderedResults()
    {
        // Given
        var sorter = new QueryFilterAndSorter<TestEntity>(_testData, "Age desc", null);

        // When
        var result = sorter.Apply().ToList();

        // Then
        Assert.Equal(4, result.First().Id);
        Assert.Equal(1, result.Last().Id);
    }

    /// <summary>
    /// Tests that filters and ordering are applied together correctly.
    /// </summary>
    [Fact(DisplayName = "Given valid filters and ordering When applied Then returns filtered and ordered results")]
    public void Apply_WithFiltersAndOrdering_ReturnsFilteredAndOrderedResults()
    {
        // Given
        var filters = new Dictionary<string, string>
        {
            { "Name", "Alice" }
        };
        var sorter = new QueryFilterAndSorter<TestEntity>(_testData, "Age asc", filters);

        // When
        var result = sorter.Apply().ToList();

        // Then
        Assert.Equal(2, result.Count);
        Assert.Equal(1, result.First().Id);
        Assert.Equal(4, result.Last().Id);
    }

    /// <summary>
    /// Tests that all results are returned when no filters or ordering are applied.
    /// </summary>
    [Fact(DisplayName = "Given no filters or ordering When applied Then returns all results")]
    public void Apply_WithoutFiltersOrOrdering_ReturnsAllResults()
    {
        // Given
        var sorter = new QueryFilterAndSorter<TestEntity>(_testData, string.Empty, null);

        // When
        var result = sorter.Apply().ToList();

        // Then
        Assert.Equal(4, result.Count);
    }

    /// <summary>
    /// Helper class for testing.
    /// </summary>
    private class TestEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
