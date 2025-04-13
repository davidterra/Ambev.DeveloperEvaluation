namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a branch entity in the system.
    /// A branch contains information such as its unique identifier, name, creation and update timestamps,
    /// and a collection of associated carts.
    /// </summary>
    public class Branch
    {
        /// <summary>
        /// Gets or sets the unique identifier for the branch.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the branch.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the branch was created.
        /// Defaults to the current UTC time.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date and time when the branch was last updated.
        /// Null if the branch has not been updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the collection of carts associated with the branch.
        /// </summary>        
        public List<Sale> Sales { get; set; } = null!;
    }
}
