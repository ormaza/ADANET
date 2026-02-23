using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class CustomExceptionFilter : IExceptionFilter
{
    private readonly ILogger<CustomExceptionFilter> _logger;
    public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception,
                        "Ocorreu uma exceção não tratada capturada pelo filtro. Caminho {Path}",
                        context.HttpContext.Request.Path);

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