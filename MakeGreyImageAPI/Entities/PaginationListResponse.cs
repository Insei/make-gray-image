namespace MakeGreyImageAPI.Entities;

    /// <summary>
    /// 
    /// </summary>
    public class Status
    {
        /// <summary>
        /// 
        /// </summary>
        public int Code { get; set; } = 0;
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; } = "";
    }
    /// <summary>
    /// 
    /// </summary>
    public class EmptyApiResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public Status Status { get; set; } = new();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T> : EmptyApiResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public T? Data { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// 
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Total { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDtoType"></typeparam>
    public class PaginatedListApiResponse<TDtoType> : ApiResponse<List<TDtoType>>
    {
        /// <summary>
        /// 
        /// </summary>
        public Pagination Pagination { get; set; } = new Pagination();
    }