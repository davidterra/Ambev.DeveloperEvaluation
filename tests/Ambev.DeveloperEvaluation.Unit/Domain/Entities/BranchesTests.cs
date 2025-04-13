using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the <see cref="Branch"/> entity.
/// </summary>
public class BranchesTests
{
    /// <summary>
    /// Tests that a new branch is initialized with default values.
    /// </summary>
    [Fact(DisplayName = "Given new branch When initialized Then sets default values")]
    public void Initialize_NewBranch_SetsDefaultValues()
    {
        // Given
        var branch = new Branch();

        // Then
        branch.Id.Should().Be(0);
        branch.Name.Should().BeEmpty();
        branch.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        branch.UpdatedAt.Should().BeNull();
    }

    /// <summary>
    /// Tests that the branch properties can be set and retrieved correctly.
    /// </summary>
    [Fact(DisplayName = "Given branch When setting properties Then retrieves correct values")]
    public void SetProperties_Branch_SetsAndRetrievesCorrectValues()
    {
        // Given       
        var branch = new Branch
        {
            Id = 1,
            Name = "Main Branch",
            CreatedAt = new DateTime(2025, 1, 1),
            UpdatedAt = new DateTime(2025, 1, 2),
        };

        // Then
        branch.Id.Should().Be(1);
        branch.Name.Should().Be("Main Branch");
        branch.CreatedAt.Should().Be(new DateTime(2025, 1, 1));
        branch.UpdatedAt.Should().Be(new DateTime(2025, 1, 2));
    }
}


