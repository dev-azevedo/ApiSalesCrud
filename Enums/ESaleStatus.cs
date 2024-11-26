using System.ComponentModel.DataAnnotations;

namespace SalesCrud.Enums;

public enum ESaleStatus
{
    [Display(Name = "Em andamento")]
    InProgress = 1,
    
    [Display(Name = "Pendente")]
    Pending = 2,

    [Display(Name = "Concluído")]
    Finished = 3,
}
