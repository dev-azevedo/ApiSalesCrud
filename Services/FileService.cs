using SalesCrud.Model;
using SalesCrud.Enums;
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
        _basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload");

        if (!Directory.Exists(_basePath))
        {
            Directory.CreateDirectory(_basePath);
        }
    }

    public FileResult GetFile(EDestinationFile destinationFile, Guid productId)
    {
        var searchPattern = $"{productId}.*"; // Padrão de busca para qualquer extensão
        var fileSavePath = Path.Combine(_basePath, destinationFile.ToString());
        var files = Directory.GetFiles(fileSavePath, searchPattern);

        if (files.Length == 0)
        {
            return null;
        }

        var filePath = files[0]; // Pega o primeiro arquivo encontrado

        return new FileResult
        {
            FileBytes = File.ReadAllBytes(filePath),
            FileName = Path.GetFileName(filePath)
        };
    }

    public async Task<FileViewModel> SaveFile(IFormFile file, Guid id, EDestinationFile destinationFile)
    {
        FileViewModel fileDetail = new();

        var fileSavePath = Path.Combine(_basePath, destinationFile.ToString());

        if (!Directory.Exists(fileSavePath))
        {
            Directory.CreateDirectory(fileSavePath);
        }


        var fileType = Path.GetExtension(file.FileName);
        var baseUrl = $"{_context.HttpContext.Request.Scheme}://{_context.HttpContext.Request.Host}{_context.HttpContext.Request.PathBase}";
        var fileTypes = new List<string> { ".jpg", ".jpeg", ".png" };

        if (fileTypes.Contains(fileType.ToLower()))
        {
            var fileName = $"{id}{fileType}";
            if ((file != null && file.Length > 0))
            {
                var destination = Path.Combine(fileSavePath, "", fileName);

                if (File.Exists(destination))
                {
                    File.Delete(destination);
                }


                fileDetail.Name = fileName;
                fileDetail.Type = fileType;
                fileDetail.Url = $"{baseUrl}/api/file/{destinationFile.GetHashCode()}/{id}";

                using var stream = new FileStream(destination, FileMode.Create);
                await file.CopyToAsync(stream);
            }
        }

        return fileDetail;
    }


    public void DeleteFile(Guid id, EDestinationFile destinationFile)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Invalid productId", nameof(id));
        }

        var searchPattern = $"{id}.*";
        var fileSavePath = Path.Combine(_basePath, destinationFile.ToString());

        if(!Directory.Exists(fileSavePath))
    {
            throw new DirectoryNotFoundException($"The directory {fileSavePath} does not exist.");
        }

        var files = Directory.GetFiles(fileSavePath, searchPattern);

        if (files.Length > 0)
        {
            var filePath = files[0];

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
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

    public string GetFilePath(EDestinationFile destinationFile, Guid id)
    {
        var directoryPath = Path.Combine(_basePath, destinationFile.ToString());

        if (Directory.Exists(directoryPath))
        {
            var files = Directory.GetFiles(directoryPath, $"{id}*");

            if (files.Length > 0)
            {
                var baseUrl = $"{_context.HttpContext.Request.Scheme}://{_context.HttpContext.Request.Host}{_context.HttpContext.Request.PathBase}";
                var filePath = $"{baseUrl}/api/file/{destinationFile.GetHashCode()}/{id}";
                return filePath;
            }

        }

        return null;
    }

    public class FileResult
    {
        public byte[] FileBytes { get; set; }
        public string FileName { get; set; }
    }
}
