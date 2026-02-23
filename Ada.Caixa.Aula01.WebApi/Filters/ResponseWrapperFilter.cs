using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ResponseWrapperFilter : IResultFilter
{
    public void OnResultExecuted(ResultExecutedContext context)
    {
        
    }

    public void OnResultExecuting(ResultExecutingContext context)
    {
        //se sucesso, ou seja, status code 2xx, então envolvo a resposta em um ResponseWrapper
        if(context.Result is ObjectResult objectResult &&
            objectResult.StatusCode >= 200 && objectResult.StatusCode < 300)
        {
            int statusCode = objectResult.StatusCode ?? 200; //se for nulo, assumo 200

            var wrappedResponse = new ResponseWrapper<object>
            {
                Status = statusCode,
                Data = objectResult.Value
            };

            context.Result = new ObjectResult(wrappedResponse)
            {
                StatusCode = statusCode
            };
        }
    }
}