using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Todo.Presentation.Api.Utils;

public class Response<T>
{
    public Response(T? data)
    {
        Data = data;
    }

    public T? Data { get; set; }
    public int Status { get; set; } = 200;
    public object? Validation { get; set; } = null;
    public string? Message { get; set; }
    public string? InnerMessage { get; set; }
}

public static class ExceptionHandler
{
    public static ObjectResult HandleArgumentNullException(this ControllerBase controllerBase, ArgumentNullException ex, int status = 400)
    {
        var message = $"{ex.Message} cannot be null";
        return controllerBase.StatusCode(status, new Response<object>(null) { Message = message, Status = status });
    }

    public static ObjectResult UnHandledException(this ControllerBase controllerBase, Exception ex, int status = 400)
    {
        return controllerBase.StatusCode(status, new Response<object>(null) { Message = ex.Message, Status = status, InnerMessage = ex.InnerException?.Message });
    }
}

