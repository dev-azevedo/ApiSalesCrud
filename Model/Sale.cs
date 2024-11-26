using SalesCrud.Enums;
using Microsoft.AspNetCore.Identity;

namespace SalesCrud.Model;

public class Sale : BaseModel
{
    public Guid ClientId { get; set; }
    public Guid ProductId { get; set; }
    public int ProductQuantity { get; set; }
    public decimal ValueSale { get; set; }
    public string UserId { get; set; }
    public ESaleStatus Status { get; set; } = ESaleStatus.InProgress;
   
    public Client Client { get; set; }
    public Product Product { get; set; }
    public IdentityUser User { get; set; }
}
