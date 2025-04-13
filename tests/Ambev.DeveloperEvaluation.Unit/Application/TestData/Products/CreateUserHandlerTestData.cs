using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Products;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateProductHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid User entities.
    /// The generated users will have valid:
    /// - Username (using internet usernames)
    /// - Password (meeting complexity requirements)
    /// - Email (valid format)
    /// - Phone (Brazilian format)
    /// - Status (Active or Suspended)
    /// - Role (Customer or Admin)
    /// </summary>
    private static readonly Faker<CreateProductCommand> createProductCommandFaker = new Faker<CreateProductCommand>()
        .RuleFor(u => u.Title, f => f.Commerce.ProductName())
        .RuleFor(u => u.Price, f => f.Random.Decimal(1, 1000))
        .RuleFor(u => u.Description, f => f.Lorem.Paragraph(2))
        .RuleFor(u => u.Category, f => f.Commerce.Categories(1)[0])
        .RuleFor(u => u.Image, f => f.Internet.Url())
        .RuleFor(u => u.Rating, f => new CreateRatingCommand
        {
            Rate = f.Random.Decimal(1, 5),
            Count = f.Random.Int(1, 100)
        });

    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static CreateProductCommand GenerateValidCommand()
    {
        return createProductCommandFaker.Generate();
    }
}
