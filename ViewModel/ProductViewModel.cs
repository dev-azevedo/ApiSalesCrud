using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SalesCrud.ViewModel;

public class ProductPostViewModel
{
    [Required(ErrorMessage = "O campo descrição é obrigatório.")]
    public string Description { get; set; }
    [Required(ErrorMessage = "O campo valor unitário é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O campo valor unitário deve ser maior que zero.")]
    public decimal UnitaryValue { get; set; }
}

public class ProductPutViewModel
{
    [Required(ErrorMessage = "O campo id é obrigatório.")]
    [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "O campo id é obrigatório.")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo descrição é obrigatório.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "O campo valor unitário é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O campo valor unitário deve ser maior que zero.")]
    public decimal UnitaryValue { get; set; }
    public string PathImage { get; set; }
}

public class ProductRespViewModel
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public decimal UnitaryValue { get; set; }
    public string PathImage { get; set; }
}
