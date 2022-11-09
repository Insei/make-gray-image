using AutoMapper;
using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.DTOs.Results;
using MakeGreyImageAPI.DTOs.Sorts;
using MakeGreyImageAPI.Entities;
using MakeGreyImageAPI.Interfaces;

namespace MakeGreyImageAPI.Services;
/// <summary>
/// Service for working with conversion Tasks
/// </summary>
public class LocalImageConvertTaskService
{
    private IGenericRepository<Guid, LocalImage> _imageRepository;
    private IGenericRepository<Guid, LocalImageConvertTask> _convertTaskRepository;
    private readonly IImageManager _manager;
    private readonly IMapper _mapper;
    private readonly IServiceProvider _scopeFactory;
    private readonly IAuthenticatedUserService _authenticatedUserService;
    
    /// <summary>
    /// Constructor of LocalImageConvertTaskService
    /// </summary>
    /// <param name="imageRepository"></param>
    /// <param name="convertTaskRepository"></param>
    /// <param name="manager">IImageManager</param>
    /// <param name="mapper">IMapper</param>
    /// <param name="scopeFactory"></param>
    /// <param name="authenticatedUserService"></param>
    public LocalImageConvertTaskService(IGenericRepository<Guid, LocalImage> imageRepository, 
        IGenericRepository<Guid, LocalImageConvertTask> convertTaskRepository, IImageManager manager,
        IMapper mapper, IServiceProvider scopeFactory, IAuthenticatedUserService authenticatedUserService)
    {
        _imageRepository = imageRepository;
        _convertTaskRepository = convertTaskRepository;
        _manager = manager;
        _mapper = mapper;
        _scopeFactory = scopeFactory;
        _authenticatedUserService = authenticatedUserService;
    }
    private async void ConvertImageCallback(Task<byte[]> imageTask, Guid taskId, Guid userId)
    {
        // This callback use repository after main scope is disposed,
        // so we need to create new scope
        using (var scope = _scopeFactory.CreateScope())
        {
            var imageRepo = scope.ServiceProvider.GetRequiredService<IGenericRepository<Guid, LocalImage>>();
            var convertTaskRepo = scope.ServiceProvider.GetRequiredService<IGenericRepository<Guid, LocalImageConvertTask>>();
            
            var image = await imageTask;
            var convertTask = await convertTaskRepo.GetById(taskId);
            var localOriginImage = await imageRepo.GetById(convertTask!.InImageId);
            var localGreyImage = new LocalImage
            {
                Name = "(Grey_Format)" + " " + localOriginImage!.Name,
                Extension = localOriginImage.Extension,
                Image = image,
                Width = localOriginImage.Width,
                Height = localOriginImage.Height,
                CreatedBy = userId
            };
            var newGreyImage = await imageRepo.Insert(localGreyImage);
        
            convertTask.OutImageId = newGreyImage.Id;
            convertTask.ConvertStatus = "Success";
            convertTask.UpdatedBy = userId;
            await convertTaskRepo.Update(convertTask);
        }
    }
    /// <summary>
    /// Creates a gray image
    /// </summary>
    /// <param name="convertImageCreate">Entity task</param>
    /// <returns>LocalImageConvertTaskDTO</returns>
    public async Task<LocalImageConvertTaskDto> Create(LocalImageConvertTaskCreateDto convertImageCreate)
    {
        var localImage = await _imageRepository.GetById(convertImageCreate.ImageId);
        var convertTask = new LocalImageConvertTask
        {
            InImageId = convertImageCreate.ImageId,
            ConvertStatus = "Image processing"
        };
        var newConvertTask = await _convertTaskRepository.Insert(convertTask);
        
        var greyImageTask = _manager.ConvertToGrey(localImage!);
        var _ = greyImageTask.ContinueWith(greyImageTask =>
        {
            ConvertImageCallback(greyImageTask, newConvertTask.Id, _authenticatedUserService.GetUserId());
        });
        
        var result = _mapper.Map<LocalImageConvertTaskDto>(newConvertTask);
        return result;
    }
    /// <summary>
    /// Update LocalImageConvertTask entity
    /// </summary>
    /// <param name="updateLocalImageConvert">New entity for updating</param>
    /// <param name="id">Entity ID</param>
    /// <returns>LocalImageConvertTaskDTO</returns>
    public async Task<LocalImageConvertTaskDto> Update(LocalImageConvertTaskDto updateLocalImageConvert, Guid id)
    {
        var imageConvertTask = await _convertTaskRepository.GetById(id);
        _mapper.Map(updateLocalImageConvert, imageConvertTask); 
        await _convertTaskRepository.Update(imageConvertTask!);
        return await _mapper.Map<Task<LocalImageConvertTaskDto>>(imageConvertTask);
    }
    /// <summary>
    /// Delete Entity
    /// </summary>
    /// <param name="id">Entity ID</param>
    public async Task Delete(Guid id)
    {
        var imageConvertTask = await _convertTaskRepository.GetById(id);
        if(imageConvertTask == null) return;
        
        if (imageConvertTask.OutImageId != null)
        {
           var greyImage = await _imageRepository.GetById((Guid) imageConvertTask.OutImageId); 
           
           if(greyImage != null)
               _imageRepository.Delete(greyImage.Id);
        }
        _convertTaskRepository.Delete(imageConvertTask.Id);
    }
    /// <summary>
    /// Get DTO Entity by ID
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <returns>LocalImageConvertTaskDTO</returns>
    public async Task<LocalImageConvertTaskDto?> GetById(Guid id)
    {
        var imageConvertTask =  await _convertTaskRepository.GetById(id);
        if (imageConvertTask == null) return null;
        var imageDto = _mapper.Map<LocalImageConvertTaskDto>(imageConvertTask);
        return imageDto;
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
    public async Task<PaginatedResult<List<LocalImageConvertTaskDto>>> GetPaginatedList(int pageNumber = 0, int pageSize = 0,
        string orderBy = "", SortDirection orderDirection = SortDirection.Asc, string search = "")
    {
        var pagination = Pagination.Generate(pageNumber,pageSize, await _convertTaskRepository.Count(search));
        var entities = await _convertTaskRepository.GetList(search, orderBy, orderDirection, 
            pagination.CurrentPage, pagination.PageSize);
        
        var result = new PaginatedResult<List<LocalImageConvertTaskDto>>(_mapper.Map<List<LocalImageConvertTaskDto>>(entities))
        {
            Pagination = pagination
        };
        return result;
    }
}