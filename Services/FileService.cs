using SalesCrud.Services.Interfaces;
using SalesCrud.ViewModel;

namespace SalesCrud.Services;

public class FileService : IFileService
{
    private readonly string _basePath;
    private readonly IHttpContextAccessor _context;

    public FileService(IHttpContextAccessor context)
    {
        _context = context;
        _basePath = Directory.GetCurrentDirectory() + "\\Upload\\Product";
    }

    public byte[] GetFile(string fileName) {

        var filePath = Path.Combine(_basePath, fileName);

        if (!File.Exists(filePath))
        {
            return null;
        }

        return File.ReadAllBytes(filePath);
    }

    public async Task<FileViewModel> SaveFile(IFormFile file, Guid ProductId)
    {
        FileViewModel fileDetail = new();

        var fileType = Path.GetExtension(file.FileName);
        var baseUrl = _context.HttpContext.Request.Host;
        var fileTypes = new List<string> { ".jpg", ".jpeg", ".png" };

        if (fileTypes.Contains(fileType.ToLower())) {
            var fileName = $"{ProductId}__{Path.GetFileName(file.FileName)}";
            if ((file != null && file.Length > 0))
            {
                var destination = Path.Combine(_basePath, "", fileName);
                fileDetail.Name = fileName;
                fileDetail.Type = fileType;
                fileDetail.Url = Path.Combine(baseUrl + "/api/file/" + fileDetail.Name);

                using var stream = new FileStream(destination, FileMode.Create);
                await file.CopyToAsync(stream);
            }
        }

        return fileDetail;
    }

    public string GetMimeType(string extension)
    {
        return extension switch
        {
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            _ => "application/octet-stream",
        };
    }
}
