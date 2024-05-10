namespace SalesCrud.Model;

public class BaseModel
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime? EditedOn { get; set; }

    public BaseModel()
    {
        Id = Guid.NewGuid();
    }
}
