using SalesCrud.Services.Enums;

namespace SalesCrud.ViewModel;

public class FileViewModel
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Url { get; set; }
}

public class FilePostViewModel
{
    public Guid Id { get; set; }
    public IFormFile File { get; set; }
}

