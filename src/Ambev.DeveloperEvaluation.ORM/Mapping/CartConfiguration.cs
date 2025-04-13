using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    /// <summary>
    /// Configures the database mapping for the <see cref="Cart"/> entity.
    /// This includes table name, primary key, property configurations, relationships, and ignored properties.
    /// </summary>
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Cart"/> for the database context.
        /// </summary>
        /// <param name="builder">The builder used to configure the <see cref="Cart"/> entity.</param>
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            // Configures the primary key for the Cart entity.
            builder.HasKey(c => c.Id);

            // Configures the Id property to be auto-generated.
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            // Configures the UserId property as a required UUID column.
            builder.Property(c => c.UserId)
                .HasColumnType("uuid")
                .IsRequired();

            // Configures the Status property with a string conversion and a maximum length of 20.
            builder.Property(c => c.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            // Configures the CreatedAt property with a default value of the current timestamp.
            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            // Configures the CanceledAt property as optional.
            builder.Property(c => c.CanceledAt)
                .IsRequired(false);

            // Configures the UpdatedAt property as optional.
            builder.Property(c => c.UpdatedAt)
                .IsRequired(false);

            // Configures the one-to-many relationship between Cart and CartProduct.
            builder.HasMany(c => c.Items)
                .WithOne(p => p.Cart)
                .HasForeignKey(p => p.CartId);

            // Configures the one-to-many relationship between User and Cart.
            builder.HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId);

        }
    }
}
