using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Exceptions;

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
    public static ObjectResult HandleNotFoundException(this ControllerBase controllerBase, NotFoundException ex, int status = 404)
    {
        var message = ex.Message.NotFoundException();

        var response = new Response<object>(null)
        {
            Message = message, 
            Status = status
        };

        return controllerBase.NotFound(response);
    }

    public static ObjectResult HandleMessageException(this ControllerBase controllerBase, MessageException ex, int status = 400)
    {
        var response = new Response<object>(null)
        {
            Message = ex.Message, 
            Status = status
        };

        return controllerBase.BadRequest(response);
    }

    public static ObjectResult UnHandledException(this ControllerBase controllerBase, Exception ex, int status = 500)
    {
        var response = new Response<object>(null)
        {
            Message = ex.Message, 
            Status = status, 
            InnerMessage = ex.InnerException?.Message
        };

        return controllerBase.StatusCode(status, response);
    }
}
