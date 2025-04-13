using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    /// <summary>
    /// Configures the database mapping for the <see cref="Product"/> entity.
    /// </summary>
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        /// <summary>
        /// Configures the properties and relationships of the <see cref="Product"/> entity.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.Category)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Image)
                .HasMaxLength(500);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(p => p.UpdatedAt);

            builder.OwnsOne(p => p.Price, mv =>
            {
                mv.Property(m => m.Amount)
                  .HasColumnName("Price")
                  .IsRequired();
            });

            builder.OwnsOne(p => p.Rating, rating =>
            {
                rating.Property(r => r.Rate)
                    .HasColumnName("RatingRate")
                    .IsRequired();

                rating.Property(r => r.Count)
                    .HasColumnName("RatingCount")
                    .IsRequired();
            });
        }
    }
}
