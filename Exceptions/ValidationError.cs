using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CamposDealerCrud.Exceptions;

public class ValidationError
{
    public string Message { get; set; }

    public ValidationError(string message)
    {
        Message = message;
    }
}