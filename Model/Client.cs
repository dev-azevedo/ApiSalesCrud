﻿using System.Text.Json.Serialization;

namespace SalesCrud.Model;

public class Client : BaseModel
{
    public string Name { get; set; }
    public string City { get; set; }
    [JsonIgnore]
    public ICollection<Sale> Sales { get; set; }
}
