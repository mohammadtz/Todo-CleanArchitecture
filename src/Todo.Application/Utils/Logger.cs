using Todo.Application.Contract.Utils;

namespace Todo.Application.Utils;

public class Logger:ILogger
{
    public string? LastError { get; set; } = null;

    public void Log(string message)
    {
        Console.WriteLine($"Logger Message: {message}");
        LastError = message;
    }
}