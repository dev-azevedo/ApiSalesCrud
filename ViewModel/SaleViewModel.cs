using System.ComponentModel.DataAnnotations;

namespace CamposDealerCrud.ViewModel;

public class SalePostViewModel
{
    [Required(ErrorMessage = "O campo cliente é obrigatório")]
    public Guid ClientId { get; set; }
    [Required(ErrorMessage = "O campo produto é obrigatório")]
    public Guid ProductId { get; set; }
    [Required(ErrorMessage = "O campo quatidade de produto é obrigatório")]
    public int ProductQuantity { get; set; }
}

public class SalePutViewModel
{
    [Required(ErrorMessage = "O campo id é obrigatório")]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "O campo cliente é obrigatório")]
    public Guid ClientId { get; set; }
    [Required(ErrorMessage = "O campo produto é obrigatório")]
    public Guid ProductId { get; set; }
    [Required(ErrorMessage = "O campo quatidade de produto é obrigatório")]
    public int ProductQuantity { get; set; }
}

public class SaleRespViewModel
{
    public Guid Id { get; set; }
    public ClientRespViewModel Client { get; set; }
    public ProductRespViewModel Product { get; set; }
    public int ProductQuantity { get; set; }
    public decimal ValueSale { get; set; }
}
