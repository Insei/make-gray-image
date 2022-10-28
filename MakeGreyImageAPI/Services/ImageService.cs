using System.Drawing;
using AutoMapper;
using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.DTOs.Results;
using MakeGreyImageAPI.DTOs.Sorts;
using MakeGreyImageAPI.Entities;
using MakeGreyImageAPI.Interfaces;

namespace MakeGreyImageAPI.Services;

/// <summary>
/// Service for working with images
/// </summary>
public class ImageService
{
    
    private readonly IGenericRepository _repository;
    private readonly IMapper _mapper;
    /// <summary>
    /// Constructor of ImageService class
    /// </summary>
    /// <param name="repository">IGenericRepository</param>
    /// <param name="mapper">IMapper</param>
    public ImageService(IGenericRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;

    }

    /// <summary>
    /// Create a new entity of image
    /// </summary>
    /// <param name="objFile">The image received from the request</param>
    /// <returns>DTO of image</returns>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public async Task <LocalImageDto> Create(IFormFile objFile)
    {
        
        if (objFile.Length == 0) return new LocalImageDto();
        var systemImage = ConvertToSystemImageFormat(objFile);
        var uploadImage = new LocalImage
        {
            Name = objFile.FileName,
            Extension = objFile.ContentType,
            Image = ConvertToBytes(objFile),
            Width = systemImage.Width,
            Height = systemImage.Height
        };
        systemImage.Dispose();
        var returningImage = await _repository.Insert(uploadImage);
        return _mapper.Map<LocalImageDto>(returningImage);
    }

    /// <summary>
    /// Get DTO Entity by ID
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <returns>DTO of image</returns>
    public async Task<LocalImageDto?> GetById(Guid id)
    {
        var image =  await _repository.GetById<LocalImage>(id);
        var imageDto = _mapper.Map<LocalImageDto>(image);
        return imageDto;
    }
    /// <summary>
    /// Delete Entity
    /// </summary>
    /// <param name="id">Entity ID</param>
    public async Task Delete(Guid id)
    {
        var image = await _repository.GetById<LocalImage>(id);
        if(image != null) _repository.Delete(image); 
    }

    /// <summary>
    /// Get paginated Entity List 
    /// </summary>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="orderBy">Sorting</param>
    /// <param name="orderDirection">Sorting direction</param>
    /// <param name="search">Search string</param>
    /// <returns>Paginated list of entities</returns>
    public async Task<PaginatedResult<List<LocalImageDto>>> GetPaginatedList(int pageNumber = 0, int pageSize = 0,
        string orderBy = "", SortDirection orderDirection = SortDirection.Asc, string search = "")
    {
        var count = await _repository.Count<LocalImage>(search);
        var pagination = Pagination.Generate(pageNumber,pageSize,count);
        var entities = await _repository.GetPaginatedList<LocalImage>(search, orderBy, orderDirection, 
            pagination.CurrentPage, pagination.PageSize);
        
        var result = new PaginatedResult<List<LocalImageDto>>
        {
            Pagination = pagination,
            Data = _mapper.Map<List<LocalImageDto>>(entities)
        };
        return result;
    }

    /// <summary>
    /// Convert IFormFile format file to byte
    /// </summary>
    /// <param name="image">Image file</param>
    /// <returns>Image in byte format</returns>
    private static byte[]? ConvertToBytes(IFormFile image)
    {
        if (image.Length <= 0) return null;
        using (var ms = new MemoryStream())
        {
            image.CopyTo(ms);
            return ms.ToArray();
        }
    }
    /// <summary>
    /// Convert IFormFile file to Image 
    /// </summary>
    /// <param name="image">Image file</param>
    /// <returns>Image format file</returns>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    private static Image ConvertToSystemImageFormat(IFormFile image)
    {
        using (var memoryStream = new MemoryStream())
        { 
            image.CopyTo(memoryStream);
            var img = Image.FromStream(memoryStream);
            return img;
        }
    }
    /// <summary>
    /// Get image in byte format
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <returns>Image in byte format</returns>
    public async Task<byte[]?> GetImageByte(Guid id)
    {
        var image = await _repository.GetById<LocalImage>(id);
        return image?.Image;
    }
}