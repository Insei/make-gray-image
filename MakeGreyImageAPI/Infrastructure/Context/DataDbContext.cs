namespace MakeGreyImageAPI.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;
using MakeGreyImageAPI.Entities;

/// <summary>
/// Derived class from dbcontext for working with data
/// </summary>
public sealed class DataDbContext : DbContext
{
    /// <summary>
    /// Constructor of DataDbContext
    /// </summary>
    /// <param name="options"> data base context options of DataDbContext type</param>
    public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) {
        
    }

    /// <summary>
    /// A data set through which we can interact with tables from the database
    /// </summary>
    public DbSet<LocalImage>? LocalImages { get; set; }
    
    /// <summary>
    /// Method for using the fluent api functionality
    /// </summary>
    /// <param name="builder">parameter of ModelBuilder class</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}