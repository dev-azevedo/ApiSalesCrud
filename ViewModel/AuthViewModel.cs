using ApiSalesCrud.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApiSalesCrud.ViewModel;

public class AuthRegisterViewModel {

    [Required(ErrorMessage = "O campo email é obrigatório.")]
    [DataType(DataType.EmailAddress)]
    public string Email  { get; set; }

    [Required(ErrorMessage = "O campo senha é obrigatório.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "O campo confirmação de senha é obrigatório.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "As senhas não coincidem")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "O campo data de nascimento é obrigatório.")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "O campo perfil é obrigatório.")]
    public ERoleUser UserRole { get; set; }
 }

 public class AuthLoginViewModel {
    [Required(ErrorMessage = "O campo email é obrigatório.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo senha é obrigatório.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
 }
