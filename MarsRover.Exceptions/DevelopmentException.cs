using System;

namespace MarsRover.Exceptions
{
    public class DevelopmentException : Exception
    {
        public DevelopmentException(string message) : this(message, null)
        {
        }

        public DevelopmentException(string message, Exception innerEx) : base(message, innerEx)
        {
        }
    }
}