namespace Todo.Application.Contract.Utils;

public interface ILogger
{
    string? LastError { get; set; }
    void Log(string message);
}
