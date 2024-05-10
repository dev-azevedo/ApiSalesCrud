using System.Text.Json.Serialization;

namespace SalesCrud.Model;

public class Sale : BaseModel
{
    public Guid ClientId { get; set; }
    public Guid ProductId { get; set; }
    public int ProductQuantity { get; set; }
    public decimal ValueSale { get; set; }
   
    public Client Client { get; set; }
    public Product Product { get; set; }
}
