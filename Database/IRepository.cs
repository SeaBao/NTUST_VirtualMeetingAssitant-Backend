using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirturlMeetingAssitant.Backend.Db;

/// <summary>
/// The IRepository Interface.
/// </summary>
/// <remarks>
/// Every table in database should implement this interface.
/// </remarks>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Get an entity by primary key.
    /// </summary>
    Task<TEntity> Get(params object[] keyValues);
    Task<IEnumerable<TEntity>> GetAll();
    /// <summary>
    /// Find the specified entities in the database.
    /// </summary>
    /// <remarks>
    /// You can use LINQ for querying.
    /// </remarks>
    IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Add an entity to the database
    /// </summary>
    Task<TEntity> Add(TEntity entity);
    /// <summary>
    /// Add entities to the database
    /// </summary>
    Task<IEnumerable<TEntity>> AddRange(IEnumerable<TEntity> entities);
    /// <summary>
    /// Update an entity in the database
    /// </summary>
    Task<TEntity> Update(TEntity entity);
    /// <summary>
    /// Remove an entity from the database
    /// </summary>
    Task Remove(TEntity entity);
    /// <summary>
    /// Remove entitiesfrom the database
    /// </summary>
    Task RemoveRange(IEnumerable<TEntity> entities);
    /// <summary>
    /// Save changes to database.
    /// </summary>
    Task SaveChangesAsync();
}

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
{
    /// <summary>
    /// The DbContext of meetings
    /// </summary>
    protected readonly MeetingContext _dbContext;

    public Repository(MeetingContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity> Add(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
        await this.SaveChangesAsync();

        return entity;
    }

    public async Task<IEnumerable<TEntity>> AddRange(IEnumerable<TEntity> entities)
    {
        await _dbContext.Set<TEntity>().AddRangeAsync(entities);
        return entities;
    }

    public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbContext.Set<TEntity>().Where(predicate);
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> Get(params object[] keyValues)
    {
        return await _dbContext.Set<TEntity>().FindAsync(keyValues);
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        try
        {
            _dbContext.Update(entity);
            await this.SaveChangesAsync();

            return entity;
        }
        catch (System.Exception ex)
        {
            throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
        }
    }

    public async Task Remove(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        await this.SaveChangesAsync();
    }

    public async Task RemoveRange(IEnumerable<TEntity> entities)
    {
        _dbContext.Set<TEntity>().RemoveRange(entities);
        await this.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}