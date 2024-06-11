using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSalesCrud.Configurations;

public class TokenConfiguration
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
    public int Minutes { get; set; }
    public int DaysToExpiry { get; set; }
}
