using System;

namespace MarsRover.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException(string message, Exception innerEx) : base(message, innerEx)
        {
        }
    }
}