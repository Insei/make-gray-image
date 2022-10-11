using MakeGreyImageAPI.DTOs.Sorts;

namespace MakeGreyImageAPI.DTOs;
/// <summary>
/// 
/// </summary>
public class Pagination
{
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    
    public Pagination(int currentPage, int pageSize, int totalPages, int totalCount)
    {
        TotalPages = totalPages;
        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }
    public Pagination()
    {
        TotalPages = 0;
        TotalCount = 0;
        CurrentPage = 1;
        PageSize = 10;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="fieldName"></param>
    /// <param name="totalCount"></param>
    /// <param name="direction"></param>
    /// <param name="search"></param>
    /// <returns></returns>
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
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class PaginatedResult<T> : ApiResponse<T>
{
    public Pagination Pagination { get; set; }

    public PaginatedResult()
    {
        Pagination = new Pagination();
    }
    
    public PaginatedResult(T data, int currentPage, int pageSize, int totalPages, int totalCount)
    {
        Pagination = new Pagination(currentPage, pageSize, totalPages, totalCount);
        Data = data;
    }
}