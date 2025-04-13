using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM.Mapping;
using Microsoft.EntityFrameworkCore;

public class TestDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;    
    public DbSet<Product> Products { get; set; } = null!;    

    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        
        modelBuilder.ApplyConfiguration(new UserConfiguration());        
        modelBuilder.ApplyConfiguration(new ProductConfiguration());        
    }
}
