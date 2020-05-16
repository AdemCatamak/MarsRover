using System;

namespace MarsRover.Exceptions
{
    public abstract class NotValidException : Exception
    {
        protected NotValidException(string message, Exception innerEx) : base(message, innerEx)
        {
        }
    }
}