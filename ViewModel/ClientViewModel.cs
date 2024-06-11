using System.ComponentModel.DataAnnotations;

namespace SalesCrud.ViewModel;

public class ClientPostViewModel
{
    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O campo email é obrigatório.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo cidade é obrigatório.")]
    public string City { get; set; }
}

public class ClientPutViewModel
{
    [Required(ErrorMessage = "O campo id do cliente é obrigatório.")]
    [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "O campo id é obrigatório.")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O campo email é obrigatório.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo cidade é obrigatório.")]
    public string City { get; set; }
    public string PathImage { get; set; }
}

public class ClientRespViewModel {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public string PathImage { get; set; }
}

