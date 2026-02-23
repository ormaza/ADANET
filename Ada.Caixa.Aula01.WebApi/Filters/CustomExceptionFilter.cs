using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        int statusCode = StatusCodes.Status500InternalServerError;
        string message = "Ocorreu um erro inesperado. Por favor, contate o suporte da API.";

        if(context.Exception is ArgumentNullException)
        {
            statusCode = StatusCodes.Status400BadRequest;
            message = "Dados inválidos foram fornecidos.";
        }

        var response = new ErrorResponse
        {
            Success = false,
            Status = statusCode,
            Message = message,
            Details = context.Exception.StackTrace
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = statusCode
        };

        context.ExceptionHandled = true;
    }
}