using MakeGreyImageAPI.Entities;
using MakeGreyImageAPI.Infrastructure.Context;
using MakeGreyImageAPI.Infrastructure.Generics;
using MakeGreyImageAPI.Interfaces;

namespace MakeGreyImageAPI.Infrastructure.Repositories;
/// <summary>
/// Repository for working with LocalImageConvertTask entities
/// </summary>
public class LocalImageConvertTaskRepository : GenericRepository<Guid, LocalImageConvertTask>
{
    private readonly Guid _userId;
    internal override IQueryable<LocalImageConvertTask> _reader { get => _writer.Where(task => task.CreatedBy == _userId); }
     /// <summary>
     /// LocalImageConvertTaskRepository constructor
     /// </summary>
     /// <param name="authenticatedUserService">IAuthenticatedUserService</param>
     /// <param name="context">DataDbContext</param>
    public LocalImageConvertTaskRepository(IAuthenticatedUserService authenticatedUserService, DataDbContext context) : base(context)
    {
        _userId = authenticatedUserService.GetUserId();
    }
}