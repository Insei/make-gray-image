namespace MakeGreyImageAPI.DTOs.Results;
/// <summary>
/// Pagination class
/// </summary>
public class Pagination
{
    /// <summary>
    /// Total number of pages
    /// </summary>
    public int TotalPages { get; set; }
    /// <summary>
    /// Total number of items
    /// </summary>
    public int TotalCount { get; set; }
    /// <summary>
    /// Current page number
    /// </summary>
    public int CurrentPage { get; set; }
    /// <summary>
    /// Size of the page
    /// </summary>
    public int PageSize { get; set; }
    /// <summary>
    /// Pagination constructor
    /// </summary>
    /// <param name="totalPages">Total number of pages</param>
    /// <param name="totalCount">Total number of items</param>
    /// <param name="currentPage">Current page number</param>
    /// <param name="pageSize">Size of the page</param>
    public Pagination(int totalPages = 0, int totalCount = 0, int currentPage = 1, int pageSize = 10)
    {
        TotalPages = totalPages;
        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }
    /// <summary>
    /// Generates pagination of entities
    /// </summary>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="totalCount">Total count</param>
    /// <returns>Pagination</returns>
    public static Pagination Generate(int pageNumber = 0, int pageSize = 0, int totalCount = 0)
    {
        var page = pageNumber > 0 ? pageNumber : 1;
        var size = pageSize > 0 ? pageSize : 10;
        var pagination = new Pagination()
        {
            CurrentPage = page,
            PageSize = size,
            TotalCount = totalCount,
            TotalPages = (int) Math.Ceiling(totalCount / (double) size)
        };
        return pagination;
    }
}
/// <summary>
/// Class pagination results
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public class PaginatedResult<T>
{
    /// <summary>
    /// Pagination entity field
    /// </summary>
    public Pagination Pagination { get; set; }
    /// <summary>
    /// A data field with a specific type
    /// </summary>
    public T? Data { get; set; }
    /// <summary>
    /// PaginatedResult constructor
    /// </summary>
    public PaginatedResult()
    {
        Pagination = new Pagination();
    }
    /// <summary>
    /// PaginatedResult constructor
    /// </summary>
    /// <param name="data">A data with a specific type</param>
    /// <param name="currentPage">Current page number</param>
    /// <param name="pageSize">Size of the page</param>
    /// <param name="totalPages">Total number of pages</param>
    /// <param name="totalCount">Total number of items</param>
    public PaginatedResult(T data, int currentPage, int pageSize, int totalPages, int totalCount)
    {
        Pagination = new Pagination(totalPages, totalCount, currentPage: currentPage, pageSize: pageSize);
        Data = data;
    }
}