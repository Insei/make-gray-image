namespace MakeGreyImageAPI.Infrastructure.Context;

/// <summary>
/// class for working with data initialisation
/// </summary>
public class SeedDataDb
{
    /// <summary>
    /// Method for data initialisation 
    /// </summary>
    /// <param name="serviceProvider">Parameter of IServiceProvider</param>
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<DataDbContext>();
        context.Database.EnsureCreated();
    }
}