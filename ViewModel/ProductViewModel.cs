using System.ComponentModel.DataAnnotations;

namespace CamposDealerCrud.ViewModel;

public class ProductPostViewModel
{
    [Required(ErrorMessage = "O campo descrição é obrigatório")]
    public string Description { get; set; }
    [Required(ErrorMessage = "O campo valor unitário é obrigatório")]
    public decimal UnitaryValue { get; set; }
}

public class ProductPutViewModel
{
    [Required(ErrorMessage = "O campo id é obrigatório")]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "O campo descrição é obrigatório")]
    public string Description { get; set; }
    [Required(ErrorMessage = "O campo valor unitário é obrigatório")]
    public decimal UnitaryValue { get; set; }
}

public class ProductRespViewModel
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public decimal UnitaryValue { get; set; }
}
