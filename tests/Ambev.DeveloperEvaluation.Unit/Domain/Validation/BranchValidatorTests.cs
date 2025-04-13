using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the <see cref="BranchValidator"/> class.
/// </summary>
public class BranchValidatorTests
{
    private readonly BranchValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="BranchValidatorTests"/> class.
    /// </summary>
    public BranchValidatorTests()
    {
        _validator = new BranchValidator();
    }

    /// <summary>
    /// Tests that a valid branch passes validation.
    /// </summary>
    [Fact(DisplayName = "Given valid branch When validating Then passes validation")]
    public void Validate_ValidBranch_PassesValidation()
    {
        // Given
        var branch = new Branch
        {
            Name = "Main Branch"
        };

        // When
        var result = _validator.TestValidate(branch);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that an empty branch name fails validation.
    /// </summary>
    [Fact(DisplayName = "Given empty branch name When validating Then fails validation")]
    public void Validate_EmptyBranchName_FailsValidation()
    {
        // Given
        var branch = new Branch
        {
            Name = string.Empty
        };

        // When
        var result = _validator.TestValidate(branch);

        // Then
        result.ShouldHaveValidationErrorFor(b => b.Name)
            .WithErrorMessage("Branch name must not be empty.");
    }

    /// <summary>
    /// Tests that a branch name shorter than 3 characters fails validation.
    /// </summary>
    [Fact(DisplayName = "Given short branch name When validating Then fails validation")]
    public void Validate_ShortBranchName_FailsValidation()
    {
        // Given
        var branch = new Branch
        {
            Name = "AB"
        };

        // When
        var result = _validator.TestValidate(branch);

        // Then
        result.ShouldHaveValidationErrorFor(b => b.Name)
            .WithErrorMessage("Branch name must be at least 3 characters long.");
    }

    /// <summary>
    /// Tests that a branch name longer than 50 characters fails validation.
    /// </summary>
    [Fact(DisplayName = "Given long branch name When validating Then fails validation")]
    public void Validate_LongBranchName_FailsValidation()
    {
        // Given
        var branch = new Branch
        {
            Name = new string('A', 51)
        };

        // When
        var result = _validator.TestValidate(branch);

        // Then
        result.ShouldHaveValidationErrorFor(b => b.Name)
            .WithErrorMessage("Branch name cannot be longer than 100 characters.");
    }
}



