using MakeGreyImageAPI.DTOs;
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
    /// <summary>
    /// Constructor of DataDbContext
    /// </summary>
    /// <param name="options"> Data base context options of DataDbContext type</param>
    public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) {
        
    }

    /// <summary>
    /// A data set through which we can interact with tables from the database
    /// </summary>
    public DbSet<LocalImage>? LocalImages { get; set; }
    /// <summary>
    /// A data set through which we can interact with tables from the database
    /// </summary>
    public DbSet<LocalImageConvertTask>? LocalImagesConvertTask { get; set; }
}