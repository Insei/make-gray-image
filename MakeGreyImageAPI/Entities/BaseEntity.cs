using MakeGreyImageAPI.Interfaces;

namespace MakeGreyImageAPI.Entities;

/// <summary>
/// A class containing a field of special entity key
/// </summary>
/// <typeparam name="TKey">Type of special entity key</typeparam>
public class KeyEntity<TKey> : IKeyEntity<TKey> where TKey :  IComparable, IComparable<TKey>, IEquatable<TKey>
{
    /// <summary>
    /// Id of the entity
    /// </summary>
#pragma warning disable CS8618
    public TKey Id { get; set; }
#pragma warning restore CS8618
}
/// <summary>
/// Entity class with information about actions with the account
/// </summary>
public class BaseEntity<TKey> : IKeyEntity<TKey>, IUserTrackedEntity, ITimeTrackedEntity where TKey : IComparable, IComparable<TKey>, IEquatable<TKey>
{
    /// <summary>
    /// Id of the entity
    /// </summary>
#pragma warning disable CS8618
    public TKey Id { get; set; }
#pragma warning restore CS8618
    /// <summary>
    /// Create by user ID
    /// </summary>
    public Guid CreatedBy { get; set; }
    /// <summary>
    /// Last modified by user ID
    /// </summary>
    public Guid? UpdatedBy { get; set; }
    /// <summary>
    /// Created at DateTime UTC
    /// </summary>
    public DateTime Created { get; set; }
    /// <summary>
    /// Last modified DateTime UTC
    /// </summary>
    public DateTime? Updated { get; set; }
}