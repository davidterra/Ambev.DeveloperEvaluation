using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Products;

public static class ProductTestData
{

    private static readonly Faker<Product> createProductFaker = new Faker<Product>()
        .RuleFor(u => u.Id, f => f.Random.Int(1, 1000))
        .RuleFor(u => u.Title, f => f.Commerce.ProductName())
        .RuleFor(u => u.Price, f => new MonetaryValue(f.Random.Decimal(1, 1000)))
        .RuleFor(u => u.Description, f => f.Lorem.Paragraph(2))
        .RuleFor(u => u.Category, f => f.Commerce.Categories(1)[0])
        .RuleFor(u => u.Image, f => f.Internet.Url())
        .RuleFor(u => u.Rating, f => new RatingValue(f.Random.Decimal(1, 5), f.Random.Int(1, 100))
        );

    public static Product GenerateValidEntity()
    {
        return createProductFaker.Generate();
    }
}
