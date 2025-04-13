using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    /// <summary>  
    /// Configures the database mapping for the <see cref="Sale"/> entity.  
    /// This includes defining primary keys, properties, relationships, and constraints.  
    /// </summary>  
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        /// <summary>  
        /// Configures the <see cref="Sale"/> entity using the provided <see cref="EntityTypeBuilder{TEntity}"/>.  
        /// Sets up primary keys, property constraints, and relationships.  
        /// </summary>  
        /// <param name="builder">The builder used to configure the <see cref="Sale"/> entity.</param>  
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            // Configure primary key  
            builder.HasKey(s => s.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            // Configure properties  
            builder.Property(c => c.UserId)
                   .HasColumnType("uuid")
                   .IsRequired();

            builder.Property(s => s.BranchId)
                   .IsRequired();

            builder.Property(s => s.Number)
                   .HasColumnType("varchar(25)")
                   .IsRequired();

            builder.Property(s => s.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP")
                   .IsRequired();

            builder.Property(s => s.UpdatedAt)
                   .IsRequired(false);

            builder.Property(s => s.CanceledAt)
                   .IsRequired(false);

            builder.OwnsOne(p => p.TotalAmount, mv =>
            {
                mv.Property(d => d.Amount)
                  .HasColumnName("TotalAmount")
                  .IsRequired();
            });

            // Configure relationships  
            builder.HasOne(s => s.User)
                   .WithMany(u => u.Sales)
                   .HasForeignKey(s => s.UserId);

            builder.HasOne(s => s.Branch)
                   .WithMany(b => b.Sales)
                   .HasForeignKey(s => s.BranchId);

            builder.HasMany(s => s.Items)
                   .WithOne(si => si.Sale)
                   .HasForeignKey(si => si.SaleId);
        }
    }
}
