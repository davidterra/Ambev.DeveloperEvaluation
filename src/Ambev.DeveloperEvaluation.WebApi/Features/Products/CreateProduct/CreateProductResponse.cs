namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// API response model for CreateProduct operation.
/// Represents the response returned after creating a product.
/// </summary>
public class CreateProductResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the product.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the product.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the image URL of the product.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rating details of the product.
    /// </summary>
    public CreateProductRatingResponse Rating { get; set; } = null!;
}

/// <summary>
/// Represents the rating details of a product.
/// </summary>
public class CreateProductRatingResponse
{
    /// <summary>
    /// Gets or sets the average rating value of the product.
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// Gets or sets the total number of ratings for the product.
    /// </summary>
    public int Count { get; set; }
}
