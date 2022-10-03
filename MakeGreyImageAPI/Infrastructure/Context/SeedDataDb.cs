namespace MakeGreyImageAPI.Infrastructure.Context;

/// <summary>
/// 
/// </summary>
public class SeedDataDb
{
    /// <summary>
    /// Method for data initialisation 
    /// </summary>
    /// <param name="serviceProvider">parameter of IServiceProvider</param>
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<DataDbContext>();
        context.Database.EnsureCreated();
    }
}