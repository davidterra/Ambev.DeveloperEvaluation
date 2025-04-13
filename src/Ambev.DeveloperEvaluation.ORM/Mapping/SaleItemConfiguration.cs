using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    /// <summary>  
    /// Configures the database mapping for the <see cref="SaleItem"/> entity.  
    /// Defines table name, primary key, properties, and relationships.  
    /// </summary>  
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        /// <summary>  
        /// Configures the <see cref="SaleItem"/> entity.  
        /// Sets up table name, property constraints, and relationships.  
        /// </summary>  
        /// <param name="builder">The builder used to configure the entity.</param>  
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            // Table configuration  
            builder.ToTable("SaleItems");

            // Primary key configuration  
            builder.HasKey(si => si.Id);

            // Property configurations  
            builder.Property(si => si.Id)
                .IsRequired();

            builder.Property(si => si.SaleId)
                .IsRequired();

            builder.Property(si => si.ProductId)
                .IsRequired();

            builder.Property(si => si.Quantity)
                .IsRequired();

            // Owned types configuration  
            builder.OwnsOne(p => p.UnitPrice, mv =>
            {
                mv.Property(d => d.Amount)
                  .HasColumnName("UnitPrice")
                  .IsRequired();
            });

            builder.OwnsOne(p => p.DiscountPercent, mv =>
            {
                mv.Property(d => d.Value)
                  .HasColumnName("DiscountPercent")
                  .IsRequired();
            });

            builder.OwnsOne(p => p.TotalAmount, mv =>
            {
                mv.Property(d => d.Amount)
                  .HasColumnName("TotalAmount")
                  .IsRequired();
            });

            builder.Property(si => si.CreatedAt)
                .IsRequired();

            builder.Property(si => si.UpdatedAt)
                .IsRequired(false);

            builder.Property(si => si.CanceledAt)
                .IsRequired(false);

            // Relationships configuration  
            builder.HasOne(si => si.Sale)
                .WithMany(s => s.Items)
                .HasForeignKey(si => si.SaleId);

            builder.HasOne(si => si.Product)
                .WithMany(p => p.SaleItems)
                .HasForeignKey(si => si.ProductId);
        }
    }
}
