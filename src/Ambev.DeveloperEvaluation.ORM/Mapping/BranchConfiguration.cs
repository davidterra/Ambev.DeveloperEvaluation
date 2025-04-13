using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    /// <summary>
    /// Configures the database mapping for the <see cref="Branch"/> entity.
    /// </summary>
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        /// <summary>
        /// Configures the properties and relationships of the <see cref="Branch"/> entity.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            // Maps the entity to the "Branches" table.
            builder.ToTable("Branches");

            // Configures the primary key for the entity.
            builder.HasKey(c => c.Id);

            // Configures the "Id" property to be auto-generated.
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            // Configures the "Name" property with a maximum length of 100 and makes it required.
            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            // Configures the "CreatedAt" property with a default value of the current timestamp and makes it required.
            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            // Configures the "UpdatedAt" property as optional.
            builder.Property(c => c.UpdatedAt)
                .IsRequired(false);

        }
    }


}
