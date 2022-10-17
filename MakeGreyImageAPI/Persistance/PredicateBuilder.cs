using System.Linq.Expressions;

namespace MakeGreyImageAPI.Persistance;
/// <summary>
/// class which constructs LINQ predicates piece by piece
/// </summary>
public class PredicateBuilder
{
    /// <summary>
    ///  method for creating an Expression that initially evaluates true
    /// </summary>
    /// <typeparam name="T">entity type</typeparam>
    /// <returns>expression</returns>
    public static Expression<Func<T, bool>> True<T>()
    {
        return _ => true;
    }
    /// <summary>
    /// method for creating an Expression that initially evaluates true
    /// </summary>
    /// <typeparam name="T">entity type</typeparam>
    /// <returns>expression</returns>
    public static Expression<Func<T, bool>> False<T>()
    {
        return _ => false;
    }
    /// <summary>
    /// class implements a keyword search
    /// </summary>
    /// <param name="keyword">searching keyword</param>
    /// <typeparam name="T">entity type</typeparam>
    /// <returns>expression</returns>
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
    /// wrapping in a new lambda expression
    /// </summary>
    /// <param name="propertyName">property name</param>
    /// <typeparam name="T">entity type</typeparam>
    /// <returns>expression</returns>
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
    /// Or conditions method
    /// </summary>
    /// <param name="expr1">first expression</param>
    /// <param name="expr2">second expression</param>
    /// <typeparam name="T">entity type</typeparam>
    /// <returns>expression</returns>
    public static Expression<Func<T, bool>> Or<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
    }
    /// <summary>
    /// And conditions method
    /// </summary>
    /// <param name="expr1">first expression</param>
    /// <param name="expr2">second expression</param>
    /// <typeparam name="T">entity type</typeparam>
    /// <returns>expression</returns>
    public static Expression<Func<T, bool>> And<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
    }
}