﻿namespace Todo.Domain.Exceptions;

public class MessageException : Exception
{
    public MessageException(string message) : base(message)
    {
    }
}
