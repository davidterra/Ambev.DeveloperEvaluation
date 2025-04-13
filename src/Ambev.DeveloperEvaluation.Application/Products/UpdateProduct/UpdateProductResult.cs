namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Represents the response returned after successfully updating a product.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the updated product,
/// along with its details such as title, price, description, category, image URL,
/// rating, and timestamps for creation and last update.
/// </remarks>
public class UpdateProductResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the product.
    /// </summary>
    /// <remarks>
    /// The title provides a brief and descriptive name for the product.
    /// </remarks>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    /// <remarks>
    /// The price is represented as a decimal value to support currency precision.
    /// </remarks>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    /// <remarks>
    /// The description provides detailed information about the product.
    /// </remarks>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the product.
    /// </summary>
    /// <remarks>
    /// The category helps in classifying the product for easier organization and search.
    /// </remarks>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the image URL of the product.
    /// </summary>
    /// <remarks>
    /// The image URL points to a visual representation of the product.
    /// </remarks>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rating of the product.
    /// </summary>
    /// <remarks>
    /// The rating includes the average rate value and the count of ratings received.
    /// </remarks>
    public UpdateProductRatingResult Rating { get; set; } = new();

    /// <summary>
    /// Gets or sets the creation date of the product.
    /// </summary>
    /// <remarks>
    /// The creation date is stored in UTC format to ensure consistency across time zones.
    /// </remarks>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated date of the product.
    /// </summary>
    /// <remarks>
    /// The last updated date is nullable and indicates when the product was last modified.
    /// </remarks>
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Represents the rating details of a product.
/// </summary>
/// <remarks>
/// The rating details include the average rate value and the total count of ratings.
/// </remarks>
public class UpdateProductRatingResult
{
    /// <summary>
    /// Gets or sets the rate value of the product.
    /// </summary>
    /// <remarks>
    /// The rate is a decimal value representing the average rating score.
    /// </remarks>
    public decimal Rate { get; set; }

    /// <summary>
    /// Gets or sets the count of ratings for the product.
    /// </summary>
    /// <remarks>
    /// The count indicates the total number of ratings received by the product.
    /// </remarks>
    public int Count { get; set; }
}
