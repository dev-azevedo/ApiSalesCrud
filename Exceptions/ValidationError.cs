using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SalesCrud.Exceptions;

public class ValidationError
{
    public string Message { get; set; }

    public ValidationError(string message)
    {
        Message = message;
    }
}