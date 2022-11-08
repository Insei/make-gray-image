using MakeGreyImageAPI.Entities;
using MakeGreyImageAPI.Infrastructure.Context;
using MakeGreyImageAPI.Infrastructure.Generics;
using MakeGreyImageAPI.Interfaces;

namespace MakeGreyImageAPI.Infrastructure.Repositories;
/// <summary>
/// Repository for working with LocalImage entities
/// </summary>
public class LocalImageRepository : GenericRepository<Guid, LocalImage>
{
    private readonly Guid _userId;
    internal override IQueryable<LocalImage> _reader { get => _writer.Where(image => image.CreatedBy == _userId); }
    /// <summary>
   /// LocalImageRepository constructor
   /// </summary>
   /// <param name="dbContext">DataDbContext</param>
   /// <param name="authenticatedUserService">IAuthenticatedUserService</param>
    public LocalImageRepository(DataDbContext dbContext, IAuthenticatedUserService authenticatedUserService) : base(dbContext)
   {
       _userId = authenticatedUserService.GetUserId();
   }
}