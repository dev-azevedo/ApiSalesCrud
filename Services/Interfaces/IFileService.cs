using SalesCrud.Services.Enums;
using SalesCrud.ViewModel;
using static SalesCrud.Services.FileService;

namespace SalesCrud.Services.Interfaces;

public interface IFileService
{
    public FileResult GetFile(EDestinationFile destinationFile, Guid id);
    public Task<FileViewModel> SaveFile(IFormFile file, Guid ProductId, EDestinationFile destinationFile);
    public string GetMimeType(string extension);
    public string GetFilePath(EDestinationFile destinationFile, Guid ProductId);
}
