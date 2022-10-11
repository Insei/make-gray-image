using System.Linq.Expressions;

namespace MakeGreyImageAPI.Persistance;
/// <summary>
/// 
/// </summary>
public class PredicateBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Expression<Func<T, bool>> True<T>()
    {
        return _ => true;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Expression<Func<T, bool>> False<T>()
    {
        return _ => false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyword"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Expression<Func<T, bool>> PredicateSearchInAllFields<T>(string keyword)
    {
        var predicate = False<T>();
        var properties = typeof(T).GetProperties();
        foreach (var propertyInfo in properties.Where(p => p.GetGetMethod()?.IsVirtual is false))
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyInfo);
            var propertyAsObject = Expression.Convert(property, typeof(object));
            var nullCheck = Expression.NotEqual(propertyAsObject, Expression.Constant(null, typeof(object)));
            var propertyAsString = Expression.Call(property, "ToString", null, null);
            var keywordExpression = Expression.Constant(keyword);
            var contains = propertyInfo.PropertyType == typeof(string) ? Expression.Call(property, "Contains", null, keywordExpression) : Expression.Call(propertyAsString, "Contains", null, keywordExpression);
            var lambda = Expression.Lambda(Expression.AndAlso(nullCheck, contains), parameter);
            predicate = Or(predicate, (Expression<Func<T, bool>>)lambda);
        }

        return predicate;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="propertyName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Expression<Func<T, object>>? ToLambda<T>(string propertyName)
    {
        // if (typeof(T)
        //     .GetProperties().Any(p => p.GetGetMethod()?.IsVirtual is false &&
        //                               p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase)))
        //     return null;
        
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyName);
        var propAsObject = Expression.Convert(property, typeof(object));

        return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="expr1"></param>
    /// <param name="expr2"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Expression<Func<T, bool>> Or<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="expr1"></param>
    /// <param name="expr2"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Expression<Func<T, bool>> And<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
    }
}