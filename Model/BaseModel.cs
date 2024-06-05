namespace SalesCrud.Model;

public class BaseModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedOn { get; set; }
    public DateTime? EditedOn { get; set; }


}
