namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// API response model for GetProduct operation.
/// This class represents the structure of the response returned by the API
/// when retrieving details of a specific product.
/// </summary>
public class GetProductResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// This is a numeric value that uniquely identifies the product in the system.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the product.
    /// This is a short descriptive name of the product.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the product.
    /// This represents the cost of the product in decimal format.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// This provides detailed information about the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the product.
    /// This indicates the classification or grouping of the product.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the image URL of the product.
    /// This is a string containing the URL of the product's image.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rating of the product.
    /// This contains details about the product's rating, including the rate value and count of ratings.
    /// </summary>
    public GetProductRatingResponse Rating { get; set; } = new();

    /// <summary>
    /// Gets or sets the creation date of the product.
    /// This is the date and time when the product was created in the system.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated date of the product.
    /// This is the date and time when the product was last modified, if applicable.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Represents the rating details of a product.
/// This class encapsulates the rate value and the count of ratings for a product.
/// </summary>
public class GetProductRatingResponse
{
    /// <summary>
    /// Gets or sets the rate value of the product.
    /// This is a decimal value representing the average rating of the product.
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// Gets or sets the count of ratings for the product.
    /// This is an integer value representing the total number of ratings received by the product.
    /// </summary>
    public int Count { get; set; }
}
