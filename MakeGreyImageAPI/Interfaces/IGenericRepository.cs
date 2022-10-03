using System.Linq.Expressions;

namespace MakeGreyImageAPI.Interfaces;

/// <summary>
/// Interface for generic repository
/// </summary>
public interface IGenericRepository
{
    /// <summary>
    /// Add new entity
    /// </summary>
    /// <param name="entity">entity for adding</param>
    public Task<TEntity> Insert<TEntity>(TEntity? entity) where TEntity : class;

    /// <summary>
    /// Get entity by ID
    /// </summary>
    /// <param name="id">entity ID</param>
    /// <returns>entity</returns>
    public Task<TEntity?> GetById<TEntity>(Guid id) where TEntity : class;
    
    /// <summary>
    /// Delete entity
    /// </summary>
    /// <param name="entity">entity for deleting</param>
    public void Delete<TEntity>(TEntity entity) where TEntity : class;

    /// <summary>
    /// Changes the accepted entity
    /// </summary>
    /// <param name="entity">changeable entity</param>
    /// <returns></returns>
    public Task<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
    /// <summary>
    /// Get Entity List
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public Task<IEnumerable<TEntity>> GetList<TEntity>(
        Expression<Func<TEntity, bool>>? expression = null) where TEntity : class;
}