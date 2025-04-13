using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{

    /// <summary>
    /// Represents a product with details such as title, price, description, category, image, and rating.
    /// This entity follows domain-driven design principles and includes business rules validation.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the product.
        /// Must not be null or empty and should be descriptive.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the price of the product.
        /// Represents the monetary value of the product.
        /// </summary>
        public MonetaryValue Price { get; set; } = MonetaryValue.Zero;

        /// <summary>
        /// Gets or sets the description of the product.
        /// Provides detailed information about the product.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the category of the product.
        /// Represents the classification or grouping of the product.
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the image URL of the product.
        /// Represents the visual representation of the product.
        /// </summary>
        public string Image { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the rating of the product.
        /// Represents the average rating and count of reviews for the product.
        /// </summary>
        public RatingValue Rating { get; set; } = RatingValue.Zero;

        /// <summary>
        /// Gets or sets the creation date of the product.
        /// Indicates when the product was first added to the system.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the last updated date of the product.
        /// Indicates the most recent modification date of the product details.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the list of cart products associated with this product.
        /// Represents the relationship between the product and carts.
        /// </summary>
        public List<CartItem> CartItems { get; set; } = null!;


        /// <summary>
        /// Validates the product entity using the ProductValidator rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - IsValid: Indicates whether all validation rules passed.
        /// - Errors: Collection of validation errors if any rules failed.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new ProductValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }

}
