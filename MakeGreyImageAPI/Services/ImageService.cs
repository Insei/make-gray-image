using System.Drawing;
using AutoMapper;
using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.Entities;
using MakeGreyImageAPI.Interfaces;

namespace MakeGreyImageAPI.Services;

/// <summary>
/// 
/// </summary>
public class ImageService
{
    
    private readonly IGenericRepository _repository;
    private readonly IMapper _mapper;
    private readonly IImageManager _manager;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="mapper"></param>
    /// <param name="manager"></param>
    public ImageService(IGenericRepository repository, IMapper mapper, IImageManager manager)
    {
        _repository = repository;
        _mapper = mapper;
        _manager = manager;

    }

    /// <summary>
    /// Create a new entity of image
    /// </summary>
    /// <param name="objFile"></param>
    /// <returns></returns>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public async Task <LocalImageDTO> Create(IFormFile objFile)
    {
        
        if (objFile.Length == 0) return new LocalImageDTO();
        var systemImage = await ConvertToSystemImageFormat(objFile);
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
        return _mapper.Map<LocalImageDTO>(returningImage);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<LocalImageDTO>? GetById(Guid id)
    {
        var image =  await _repository.GetById<LocalImage>(id);
        if (image == null) throw new Exception("Entity not found"); 
        var imageDto = _mapper.Map<LocalImageDTO>(image);
        return imageDto;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="updateImage"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<LocalImageDTO> Update(LocalImageUpdateDTO updateImage, Guid id)
    {
        var image = _repository.GetById<LocalImage>(id);
        await _mapper.Map(updateImage, image); 
        await _repository.Update(image.Result!);
        return await _mapper.Map<Task<LocalImageDTO>>(image);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    public async Task Delete(Guid id)
    {
        var image = await _repository.GetById<LocalImage>(id);
        if(image == null) throw new Exception("Entity not found"); 
        _repository.Delete(image);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<List<LocalImageDTO>> GetList(string search)
    {
        var entities = await _repository.GetList<LocalImage>(image => image.Name.Contains(search));
        var result = _mapper.Map<List<LocalImageDTO>>(entities);
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    private static Task <Image> ConvertToSystemImageFormat(IFormFile image)
    {
        using (var memoryStream = new MemoryStream())
        { 
            image.CopyTo(memoryStream);
            var img = Image.FromStream(memoryStream);
            return Task.FromResult(img);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<byte[]?> GetImageByte(Guid id)
    {
        var image = await _repository.GetById<LocalImage>(id);
        return image?.Image;
    }
}