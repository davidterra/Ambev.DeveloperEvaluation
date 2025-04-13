namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Represents a request to create a new product in the system.
/// </summary>
public class CreateProductRequest
{
    /// <summary>
    /// Gets or sets the title of the product.
    /// This is a required field and represents the name of the product.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the product.
    /// The price must be a positive decimal value.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// Provides additional details about the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the product.
    /// Represents the classification or grouping of the product.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the image URL of the product.
    /// This should be a valid URL pointing to the product's image.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rating of the product.
    /// Contains details about the product's rating, including rate value and count.
    /// </summary>
    public CreateRatingRequest Rating { get; set; } = new();
}

/// <summary>
/// Represents the rating details of a product.
/// </summary>
public class CreateRatingRequest
{
    /// <summary>
    /// Gets or sets the rate value of the product.
    /// This is a decimal value representing the average rating.
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// Gets or sets the count of ratings for the product.
    /// Indicates the total number of ratings received.
    /// </summary>
    public int Count { get; set; }
}
