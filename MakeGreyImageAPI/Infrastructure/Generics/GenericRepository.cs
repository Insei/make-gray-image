using System.Linq.Expressions;
using MakeGreyImageAPI.Infrastructure.Context;
using MakeGreyImageAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MakeGreyImageAPI.Infrastructure.Generics;
/// <summary>
/// 
/// </summary>
public class GenericRepository : IGenericRepository
{
    /// <summary>
    /// context of DB Context
    /// </summary>
    private readonly DataDbContext _dbContext;

    /// <summary>
    /// Constructor of GenericRepository
    /// </summary>
    /// <param name="dbContext">parameter of DataDbContext</param>
    public GenericRepository(DataDbContext dbContext)
    {
        _dbContext = dbContext;
    }   
    /// <summary>
    /// Add new Entity
    /// </summary>
    /// <param name="entity">Entity for Add</param>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <returns>Created Entity</returns>
    public async Task<TEntity> Insert<TEntity>(TEntity? entity) where TEntity : class
    {
        _dbContext.Set<TEntity>().Add(entity!);
        _dbContext.Entry(entity!).State = EntityState.Added;
        await _dbContext.SaveChangesAsync();
        return entity!;
    }
    /// <summary>
    /// Get Entity by ID
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <returns>Entity</returns>
    public async Task<TEntity?> GetById<TEntity>(Guid id) where TEntity : class
    {
        var entity = await _dbContext.Set<TEntity>().FindAsync(id);
        return entity;
    }
    /// <summary>
    /// Delete Entity
    /// </summary>
    /// <param name="entity">deleting entity</param>
    /// <typeparam name="TEntity">entity type</typeparam>
    public async void Delete<TEntity>(TEntity entity) where TEntity : class
    {
        _dbContext.Set<TEntity>().Remove(entity); 
        await _dbContext.SaveChangesAsync();
    }
    /// <summary>
    /// Updating entity
    /// </summary>
    /// <param name="entity">new entity for updating</param>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <returns>updated entity</returns>
    public async Task<TEntity> Update<TEntity>(TEntity entity) where TEntity : class
    {
        _dbContext.Set<TEntity>().Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return entity;
    }
    /// <summary>
    /// Getting list of entities
    /// </summary>
    /// <param name="expression"></param>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <returns>list of entities</returns>
    public async Task<IEnumerable<TEntity>> GetList<TEntity>(Expression<Func<TEntity, bool>>? expression = null) where TEntity : class
    {
        IQueryable<TEntity> query = _dbContext.Set<TEntity>();
        if (expression != null)
            query = query.Where(expression);
        return await query.ToListAsync();
    }
}