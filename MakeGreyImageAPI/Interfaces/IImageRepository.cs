using System.Linq.Expressions;
using MakeGreyImageAPI.Entities;

namespace MakeGreyImageAPI.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IImageRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="image"></param>
    public Task<LocalImage> Insert(LocalImage? image);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<LocalImage?> GetById(Guid id);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="image"></param>
    public void Delete(LocalImage image);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public Task<LocalImage> Update(LocalImage image);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public Task<List<LocalImage>> GetList(Expression<Func<LocalImage, bool>>? expression = null);

}