using SalesCrud.ViewModel;

namespace SalesCrud.Services.Interfaces;

public interface IFileService
{
    public byte[] GetFile(string fileName);
    public Task<FileViewModel> SaveFile(IFormFile file, Guid ProductId);
    public string GetMimeType(string extension);
}
