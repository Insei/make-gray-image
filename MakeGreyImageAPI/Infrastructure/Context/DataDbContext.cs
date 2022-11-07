using MakeGreyImageAPI.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MakeGreyImageAPI.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;
using Entities;

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
        UserAuditing();
        DateTracking();
        return base.SaveChangesAsync(cancellationToken);
    }
    /// <summary>
    /// Method which called when entity saved
    /// </summary>
    /// <returns>The number of state entries written to the database</returns>
    public override int SaveChanges()
    {
        UserAuditing();
        DateTracking();
        return base.SaveChanges();
    }
    /// <summary>
    /// Method for auditing operations performed with the user account
    /// </summary>
    private void UserAuditing() 
    {
        foreach (var entry in ChangeTracker.Entries<IUserTrackedEntity<Guid>>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    var createUserId = _authenticatedService.GetUserId();
                    if(Guid.Empty != createUserId)
                        entry.Entity.CreatedBy = createUserId;
                    break;
                case EntityState.Modified:
                    var updateUserId = _authenticatedService.GetUserId();
                    if(Guid.Empty != updateUserId)
                        entry.Entity.UpdatedBy = updateUserId;
                    break;
            }
        }
    }
    /// <summary>
    /// Method for setting the desired date format
    /// </summary>
    private void DateTracking()
    {
        foreach (var entry in ChangeTracker.Entries<ITimeTrackedEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.Updated = DateTime.UtcNow;
                    break;
            }
        }
    }
}