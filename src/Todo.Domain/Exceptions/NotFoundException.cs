using System.Runtime.Serialization;

namespace Todo.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string key) : base(key)
    {
    }
}
