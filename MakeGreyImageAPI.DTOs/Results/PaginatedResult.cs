namespace MakeGreyImageAPI.DTOs.Results;
/// <summary>
/// Pagination class
/// </summary>
public class Pagination
{
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    
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
    /// <param name="pageNumber">page number</param>
    /// <param name="pageSize">page size</param>
    /// <param name="totalCount">total count</param>
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
/// <typeparam name="T">entity type</typeparam>
public class PaginatedResult<T>
{
    public Pagination Pagination { get; set; }
    public T Data { get; set; }
    public PaginatedResult()
    {
        Pagination = new Pagination();
    }
    
    public PaginatedResult(T data, int currentPage, int pageSize, int totalPages, int totalCount)
    {
        Pagination = new Pagination(totalPages, totalCount, currentPage: currentPage, pageSize: pageSize);
        Data = data;
    }
}