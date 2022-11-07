namespace MakeGreyImageAPI.Interfaces;

/// <summary>
/// Interface containing fields with id the user who created and modified the entity
/// </summary>
public interface IUserTrackedEntity<TUserKey> where TUserKey :  IComparable, IComparable<TUserKey>, IEquatable<TUserKey>
{
    /// <summary>
    /// Create by user ID
    /// </summary>
    public TUserKey CreatedBy { get; set; }
    /// <summary>
    /// Last modified by user ID
    /// </summary>
    public TUserKey UpdatedBy { get; set; }
}