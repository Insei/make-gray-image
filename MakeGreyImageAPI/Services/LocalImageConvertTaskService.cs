using AutoMapper;
using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.Entities;
using MakeGreyImageAPI.Interfaces;

namespace MakeGreyImageAPI.Services;
/// <summary>
/// 
/// </summary>
public class ConversionImageTaskService
{
    private IGenericRepository _repository;
    private IImageManager _manager;
    private IMapper _mapper;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="manager"></param>
    public ConversionImageTaskService(IGenericRepository repository, IImageManager manager, IMapper mapper)
    {
        _repository = repository;
        _manager = manager;
        _mapper = mapper;
    }

    private async void ConvertImageCallback(Task<byte[]> imageTask, Guid taskId)
    {
        var image = await imageTask;
        var greyImage = new GreyImage()
        {
            Image = image
        };
        var returnGreyImage = await _repository.Insert(greyImage);
        var convertImage = new LocalImageConvertTask()
        {
            InImageId = taskId,
            OutImageId = returnGreyImage.Id,
            ConvertStatus = "The image is converted"
        };
        await _repository.Update(convertImage);
    } 
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="convertImageCreate"></param>
    /// <returns></returns>
    public async Task<LocalImageConvertTaskDTO> Create(LocalImageConvertTaskCreateDTO convertImageCreate)
    {
        var localImage = await _repository.GetById<LocalImage>(convertImageCreate.ImageId);
        
        var convertImage = new LocalImageConvertTask()
        {
            InImageId = convertImageCreate.ImageId,
            ConvertStatus = "Image in process of converting"
        };
        var returnConvertImage = _repository.Insert(convertImage);
        
        var greyImageTask = _manager.ConvertToGrey(localImage!);
        var _ = greyImageTask.ContinueWith((task) => {ConvertImageCallback(task, localImage!.Id);});
        
        var result = _mapper.Map<LocalImageConvertTaskDTO>(returnConvertImage);
        result.Parameters!.Color = "Grey";
        result.Parameters.Extension = localImage!.Extension;
        
        return result;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="updateLocalImageConvert"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<LocalImageConvertTaskDTO> Update(LocalImageConvertTaskDTO updateLocalImageConvert, Guid id)
    {
        var imageConvertTask = _repository.GetById<LocalImageConvertTask>(id);
        await _mapper.Map(updateLocalImageConvert, imageConvertTask); 
        await _repository.Update(imageConvertTask.Result!);
        return await _mapper.Map<Task<LocalImageConvertTaskDTO>>(imageConvertTask);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public async void Delete(Guid id)
    {
        var imageConvertTask = await _repository.GetById<LocalImageConvertTask>(id);
        if(imageConvertTask == null) throw new Exception("Entity not found"); 
        _repository.Delete(imageConvertTask);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
    /// <param name="search"></param>
    /// <returns></returns>
    public async Task<List<LocalImageConvertTaskDTO>> GetList(string search)
    {
        var entities = await _repository.GetList<LocalImageConvertTask>
            (imageConvertTask => imageConvertTask.ConvertStatus.Contains(search));
        var result = _mapper.Map<List<LocalImageConvertTaskDTO>>(entities);
        return result;
    }
}