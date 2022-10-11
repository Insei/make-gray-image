using AutoMapper;
using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.DTOs.Sorts;
using MakeGreyImageAPI.Entities;
using MakeGreyImageAPI.Interfaces;

namespace MakeGreyImageAPI.Services;
/// <summary>
/// service for working with conversion Tasks
/// </summary>
public class LocalImageConvertTaskService
{
    private IGenericRepository _repository;
    private IImageManager _manager;
    private IMapper _mapper;
    
    /// <summary>
    /// Constructor of LocalImageConvertTaskService
    /// </summary>
    /// <param name="repository">IGenericRepository</param>
    /// <param name="manager">IImageManager</param>
    /// <param name="mapper">IMapper</param>
    public LocalImageConvertTaskService(IGenericRepository repository, IImageManager manager, IMapper mapper)
    {
        _repository = repository;
        _manager = manager;
        _mapper = mapper;
    }
    
    private async void ConvertImageCallback(Task<byte[]> imageTask, Guid taskId)
    {
        var image = await imageTask;
        var convertTask = await _repository.GetById<LocalImageConvertTask>(taskId);
        var localOriginImage = await _repository.GetById<LocalImage>(convertTask!.InImageId);
        var localGreyImage = new LocalImage()
        {
            Name = "(Grey_Format)" + " " + localOriginImage!.Name,
            Extension = localOriginImage.Extension,
            Image = image,
            Width = localOriginImage.Width,
            Height = localOriginImage.Height
        };
        var newGreyImage = await _repository.Insert(localGreyImage);
        
        convertTask!.OutImageId = newGreyImage.Id;
        convertTask.ConvertStatus = "Success";
        await _repository.Update(convertTask);
    } 
    
    /// <summary>
    /// Creates a gray image
    /// </summary>
    /// <param name="convertImageCreate">entity task</param>
    /// <returns>LocalImageConvertTaskDTO</returns>
    public async Task<LocalImageConvertTaskDTO> Create(LocalImageConvertTaskCreateDTO convertImageCreate)
    {
        var localImage = await _repository.GetById<LocalImage>(convertImageCreate.ImageId);
        
        var convertTask = new LocalImageConvertTask
        {
            InImageId = convertImageCreate.ImageId,
            ConvertStatus = "Image processing"
        };
        var newConvertTask = await _repository.Insert(convertTask);
        
        var greyImageTask = _manager.ConvertToGrey(localImage!);
        var _ = greyImageTask.ContinueWith(greyImageTask => {ConvertImageCallback(greyImageTask, newConvertTask!.Id);});
        
        var result = _mapper.Map<LocalImageConvertTaskDTO>(newConvertTask);
        result.Parameters!.Color = "Grey";
        result.Parameters.Extension = localImage!.Extension;
        
        return result;
    }
    
    /// <summary>
    /// Update LocalImageConvertTask entity
    /// </summary>
    /// <param name="updateLocalImageConvert">new entity for updating</param>
    /// <param name="id">entity ID</param>
    /// <returns>LocalImageConvertTaskDTO</returns>
    public async Task<LocalImageConvertTaskDTO> Update(LocalImageConvertTaskDTO updateLocalImageConvert, Guid id)
    {
        var imageConvertTask = await _repository.GetById<LocalImageConvertTask>(id);
        _mapper.Map(updateLocalImageConvert, imageConvertTask); 
        await _repository.Update(imageConvertTask!);
        return await _mapper.Map<Task<LocalImageConvertTaskDTO>>(imageConvertTask);
    }
    
    /// <summary>
    /// Delete Entity
    /// </summary>
    /// <param name="id">entity ID</param>
    /// <exception cref="Exception"></exception>
    public async Task Delete(Guid id)
    {
        var imageConvertTask = await _repository.GetById<LocalImageConvertTask>(id);
        if(imageConvertTask == null) throw new Exception("Entity not found"); 
        _repository.Delete(imageConvertTask);
    }
    
    /// <summary>
    /// Get DTO Entity by ID
    /// </summary>
    /// <param name="id">entity ID</param>
    /// <returns>LocalImageConvertTaskDTO</returns>
    public async Task<LocalImageConvertTaskDTO> GetById(Guid id)
    {
        var imageConvertTask =  await _repository.GetById<LocalImageConvertTask>(id);
        if (imageConvertTask == null) throw new Exception("Entity not found"); 
        var imageDto = _mapper.Map<LocalImageConvertTaskDTO>(imageConvertTask);
        return imageDto;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="fieldName"></param>
    /// <param name="direction"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    public async Task<PaginatedResult<List<LocalImageConvertTaskDTO>>> GetPaginatedList(int pageNumber = 0, int pageSize = 0,
        string fieldName = "", SortDirection direction = SortDirection.Asc, string search = "")
    {
        var pagination = Pagination.Generate(pageNumber,pageSize, await _repository.Count<LocalImageConvertTask>(search));
        var entities = await _repository.GetPaginatedList<LocalImageConvertTask>(search, fieldName, direction, 
            pagination.CurrentPage, pagination.PageSize);
        
        var result = new PaginatedResult<List<LocalImageConvertTaskDTO>>
        {
            Pagination = pagination,
            Data = _mapper.Map<List<LocalImageConvertTaskDTO>>(entities)
        };
        return result;
    }
}