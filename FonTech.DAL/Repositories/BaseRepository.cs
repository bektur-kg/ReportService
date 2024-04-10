using FonTech.Domain.Interfaces.Repositories;

namespace FonTech.DAL.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _dbContext;

    public BaseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        if(entity is null)
            throw new ArgumentNullException(nameof(entity));

        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>();
    }

    public async Task<TEntity> RemoveAsync(TEntity entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }
}
