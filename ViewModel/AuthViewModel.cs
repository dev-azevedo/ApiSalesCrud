using System.ComponentModel.DataAnnotations;

namespace ApiSalesCrud.ViewModel;

public class AuthRegisterViewModel {
    public string Username { get; set; }

    [Required(ErrorMessage = "O campo email é obrigatório.")]
    [DataType(DataType.EmailAddress)]
    public string Email  { get; set; }

    [Required(ErrorMessage = "O campo senha é obrigatório.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "O campo confirmação de senha é obrigatório.")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
 }

 public class AuthLoginViewModel {
    [Required(ErrorMessage = "O campo email é obrigatório.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo senha é obrigatório.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
 }
