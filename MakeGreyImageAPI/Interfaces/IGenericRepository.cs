using System.Linq.Expressions;
using MakeGreyImageAPI.DTOs.Sorts;

namespace MakeGreyImageAPI.Interfaces;

/// <summary>
/// Interface for generic repository
/// </summary>
public interface IGenericRepository
{
    /// <summary>
    /// Add new entity
    /// </summary>
    /// <param name="entity">Entity for adding</param>
    public Task<TEntity> Insert<TEntity>(TEntity entity) where TEntity : class;

    /// <summary>
    /// Get entity by ID
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <returns>entity</returns>
    public Task<TEntity?> GetById<TEntity>(Guid id) where TEntity : class;
    
    /// <summary>
    /// Delete entity
    /// </summary>
    /// <param name="entity">Entity for deleting</param>
    public void Delete<TEntity>(TEntity entity) where TEntity : class;

    /// <summary>
    /// Changes the accepted entity
    /// </summary>
    /// <param name="entity">New entity for updating</param>
    /// <returns>Updated entity</returns>
    public Task<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
    /// <summary>
    /// Get entity list
    /// </summary>
    /// <param name="expression">Filter</param>
    /// <param name="orderBy">Sorting</param>
    /// <param name="includes">Includes</param>
    /// <param name="page">Current page</param>
    /// <param name="pageSize">Page size</param>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <returns>List of entities</returns>
    public Task<IEnumerable<TEntity>> GetPaginatedList<TEntity>(Expression<Func<TEntity, bool>>? expression = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null, int page = 0,
        int pageSize = 0) where TEntity : class;
    /// <summary>
    /// Get Entity List (search in all fields)
    /// </summary>
    /// <param name="search">Search string</param>
    /// <param name="orderBy">Sorting</param>
    /// <param name="orderDirection">Sorting direction</param>
    /// <param name="page">Current page</param>
    /// <param name="pageSize">Page size</param>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <returns>List of entities</returns>
    public Task<IEnumerable<TEntity>> GetPaginatedList<TEntity>(string search = "", string orderBy = "",
        SortDirection orderDirection = SortDirection.Asc,
        int page = 0, int pageSize = 0) where TEntity : class;
    /// <summary>
    /// Count Elements with specified filter
    /// </summary>
    /// <param name="expression">Filter</param>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <returns>Number of entities</returns>
    public Task<int> Count<TEntity>(Expression<Func<TEntity, bool>>? expression = null) where TEntity : class;

    /// <summary>
    /// Count Elements with search string (search in all fields)
    /// </summary>
    /// <param name="search">Search string</param>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <returns>Number of entities</returns>
    public Task<int> Count<TEntity>(string search)
        where TEntity : class;
}