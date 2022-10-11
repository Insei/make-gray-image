using System.Linq.Expressions;
using MakeGreyImageAPI.DTOs.Sorts;
using MakeGreyImageAPI.Infrastructure.Context;
using MakeGreyImageAPI.Interfaces;
using MakeGreyImageAPI.Persistance;
using Microsoft.EntityFrameworkCore;

namespace MakeGreyImageAPI.Infrastructure.Generics;
/// <summary>
/// generalized Repository class type 
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
    /// 
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="orderBy"></param>
    /// <param name="includes"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public async Task<IEnumerable<TEntity>> GetPaginatedList<TEntity>(Expression<Func<TEntity, bool>>? expression = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, 
            IQueryable<TEntity>>? includes = null, int page = 0, int pageSize = 0) where TEntity : class
    {
        IQueryable<TEntity> query = _dbContext.Set<TEntity>();
        if (expression != null)
            query = query.Where(expression);
        if (includes != null)
            query = includes(query);
        
        if (orderBy != null)
            query = orderBy(query);

        if (page > 0 && pageSize > 0)
            query = query.Skip(pageSize * (page - 1)).Take(pageSize);
        
        return await query.ToListAsync();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="search"></param>
    /// <param name="orderBy"></param>
    /// <param name="orderDirection"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IEnumerable<TEntity>> GetPaginatedList<TEntity>(string search = "", string orderBy = "",
        SortDirection orderDirection = SortDirection.Asc, int page = 0, int pageSize = 0) where TEntity : class
    {
        var pageContext = page < 1 ? 0 : page;
        var pageSizeContext = pageSize < 1 ? 0 : pageSize;
        Expression<Func<TEntity, bool>>? filterExpression = null;
        Expression<Func<TEntity, object>>? orderExpression = null;

        if (!string.IsNullOrEmpty(search))
        {
            filterExpression = PredicateBuilder.PredicateSearchInAllFields<TEntity>(search);
        }

        IEnumerable<TEntity>? entities = null;
        if (!string.IsNullOrEmpty(orderBy))
        {
            orderExpression = PredicateBuilder.ToLambda<TEntity>(orderBy);
        }

        if (orderExpression != null)
        { 
            entities = orderDirection switch
            {
                SortDirection.Asc => await GetPaginatedList(filterExpression, c => c.OrderBy(orderExpression),null,
                    pageContext, pageSizeContext),
                SortDirection.Desc => await GetPaginatedList(filterExpression, c => c.OrderByDescending(orderExpression),null,
                    pageContext, pageSizeContext),
                _ => throw new ArgumentOutOfRangeException(nameof(orderDirection), orderDirection, null)
            };
        }
        return entities ?? await GetPaginatedList(filterExpression, null, null, page, pageSize);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="expression"></param>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public async Task<int> Count<TEntity>(Expression<Func<TEntity, bool>>? expression = null) where TEntity : class
    {
        if (expression != null)
            return await _dbContext.Set<TEntity>().Where(expression).CountAsync();
        return await _dbContext.Set<TEntity>().CountAsync();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="search"></param>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public async Task<int> Count<TEntity>(string search) where TEntity : class
    {
        Expression<Func<TEntity, bool>>? filterExpression = null;
        
        if (!string.IsNullOrEmpty(search))
        {
            filterExpression = PredicateBuilder.PredicateSearchInAllFields<TEntity>(search);
        }
        return await Count(filterExpression);
    }
}