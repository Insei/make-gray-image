using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MakeGreyImageAPI.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;
using MakeGreyImageAPI.Entities;

/// <summary>
/// Derived class from dbcontext for working with data
/// </summary>
public sealed class DataDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>,
    IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    private readonly IAuthenticatedUserService _authenticatedService;
    /// <summary>
    /// Constructor of DataDbContext
    /// </summary>
    /// <param name="options">Data base context options of DataDbContext type</param>
    /// <param name="authenticatedService">Users authenticated service</param>
    public DataDbContext(DbContextOptions<DataDbContext> options, IAuthenticatedUserService authenticatedService) : base(options)
    {
        _authenticatedService = authenticatedService;
    }

    /// <summary>
    /// A data set through which we can interact with tables from the database
    /// </summary>
    public DbSet<LocalImage>? LocalImages { get; set; }
    /// <summary>
    /// A data set through which we can interact with tables from the database
    /// </summary>
    public DbSet<LocalImageConvertTask>? LocalImagesConvertTask { get; set; }
    
    /// <summary>
    /// Method which called when entity saved
    /// </summary>
    /// <param name="cancellationToken">token</param>
    /// <returns>A task that represents the asynchronous save operation.
    /// The task result contains the number of state entries written to the database</returns>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = DateTime.UtcNow;
                    var createUserId = _authenticatedService.GetUserId();
                    if(Guid.Empty != createUserId)
                        entry.Entity.CreatedBy = createUserId;
                    break;
                case EntityState.Modified:
                    entry.Entity.Updated = DateTime.UtcNow;
                    var updateUserId = _authenticatedService.GetUserId();
                    if(Guid.Empty != updateUserId)
                        entry.Entity.UpdatedBy = updateUserId;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}