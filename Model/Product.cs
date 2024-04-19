using System.Text.Json.Serialization;

namespace CamposDealerCrud.Model;

public class Product : BaseModel
{
    public string Description { get; set; }
    public decimal UnitaryValue { get; set; }
    [JsonIgnore]
    public ICollection<Sale> Sales { get; set; }
}
