using System.Linq.Expressions;
using MakeGreyImageAPI.Entities;
using MakeGreyImageAPI.Infrastructure.Context;
using MakeGreyImageAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MakeGreyImageAPI.Infrastructure.Repositories;

/// <summary>
/// 
/// </summary>
public class ImageRepository : IImageRepository
{
    private readonly DataDbContext _dbContext;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    public ImageRepository(DataDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public async Task<LocalImage> Insert(LocalImage? image)
    {
        _dbContext.Set<LocalImage>().Add(image!);
        _dbContext.Entry(image!).State = EntityState.Added;
        await _dbContext.SaveChangesAsync();
        return image!;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<LocalImage?> GetById(Guid id)
    {
        var image = await _dbContext.LocalImages?.FirstAsync(p => p.Id == id)!;
        return image;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="image"></param>
    public async void Delete(LocalImage image)
    {
        _dbContext.LocalImages?.Remove(image); 
        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public async Task<LocalImage> Update(LocalImage image)
    {
        _dbContext.LocalImages?.Attach(image);
        _dbContext.Entry(image).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return image;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public async Task<List<LocalImage>> GetList(Expression<Func<LocalImage, bool>>? expression = null)
    {
        IQueryable<LocalImage> query = _dbContext.Set<LocalImage>();
        if (expression != null)
             query = query.Where(expression);
        return await query.ToListAsync();
    }
}