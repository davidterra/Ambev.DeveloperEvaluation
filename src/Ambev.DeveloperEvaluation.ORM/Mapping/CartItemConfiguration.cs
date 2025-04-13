using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    /// <summary>
    /// Configures the database mapping for the <see cref="CartItem"/> entity.
    /// This class defines the table name, primary key, relationships, and property configurations.
    /// </summary>
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        /// <summary>
        /// Configures the entity of type <see cref="CartItem"/>.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");

            // Configures the primary key for the CartProduct entity.
            builder.HasKey(cp => cp.Id);

            // Configures the Id property to be auto-generated.
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            // Configures a unique index on the combination of CartId and ProductId.
            builder.HasIndex(cp => new { cp.CartId, cp.ProductId })
                .IsUnique();

            // Configures the Quantity property as required.
            builder.Property(cp => cp.Quantity)
                .IsRequired();

            // Configures the CreatedAt property with a default value of the current timestamp.
            builder.Property(cp => cp.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            // Configures the UpdatedAt property as optional.
            builder.Property(cp => cp.UpdatedAt)
                .IsRequired(false);

            // Configures the CanceledAt property as optional.
            builder.Property(cp => cp.CanceledAt)
                .IsRequired(false);

            // Configures the relationship between CartProduct and Cart.
            builder.HasOne(cp => cp.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(cp => cp.CartId);

            // Configures the relationship between CartProduct and Product.
            builder.HasOne(cp => cp.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(cp => cp.ProductId);

            // Configures the owned property DiscountAmount.
            builder.OwnsOne(p => p.DiscountPercent, da =>
            {
                da.Property(d => d.Value)
                  .HasColumnName("DiscountAmount")
                  .IsRequired();
            });

            // Configures the owned property UnitPrice.
            builder.OwnsOne(p => p.UnitPrice, up =>
            {
                up.Property(u => u.Amount)
                  .HasColumnName("UnitPrice")
                  .IsRequired();
            });

            // Configures the owned property TotalAmount.
            builder.OwnsOne(p => p.TotalAmount, upwd =>
            {
                upwd.Property(u => u.Amount)
                    .HasColumnName("TotalAmount")
                    .IsRequired();
            });

        }
    }
}
