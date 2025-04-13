namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Represents a request to update an existing product in the system.
/// </summary>
public class UpdateProductRequest
{
    /// <summary>
    /// The unique identifier of the product to update.
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
    public UpdateRatingRequest Rating { get; set; } = new();
}

/// <summary>
/// Represents the rating details of a product to be updated.
/// </summary>
public class UpdateRatingRequest
{
    /// <summary>
    /// Gets or sets the rate value of the product.
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// Gets or sets the count of ratings for the product.
    /// </summary>
    public int Count { get; set; }
}
