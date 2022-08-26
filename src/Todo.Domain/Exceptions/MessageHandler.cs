namespace Todo.Domain.Exceptions;

public static class MessageHandler
{
    public static string NotFoundException(this string key) => $"{key} is not found";
    public static string IsCreated(this string key) => $"{key} is created";
    public static string IsUpdated(this string key) => $"{key} is updated";
    public static string IsDeleted(this string key) => $"{key} is deleted";
    public static string InValid(this string key) => $"{key} is invalid";
}