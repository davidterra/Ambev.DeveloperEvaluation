using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration;

/// <summary>
/// Contains generic integration tests for repositories.
/// </summary>
public abstract class RepositoryIntegrationTests<TContext, TEntity>
    where TContext : DbContext
    where TEntity : class
{
    protected abstract TContext CreateDbContext();
    protected abstract DbSet<TEntity> GetDbSet(TContext context);

    [Fact(DisplayName = "Given entity When added Then persists in database")]
    public async Task AddEntity_PersistsInDatabase()
    {
        // Given
        var context = CreateDbContext();
        var dbSet = GetDbSet(context);
        var entity = CreateEntity();

        // When
        dbSet.Add(entity);
        await context.SaveChangesAsync();

        // Then
        var persistedEntity = await dbSet.FindAsync(GetEntityKey(entity));
        Assert.NotNull(persistedEntity);
    }

    protected abstract TEntity CreateEntity();
    protected abstract object GetEntityKey(TEntity entity);
}
