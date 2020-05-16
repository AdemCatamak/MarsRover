using System;
using MarsRover.Exceptions;

namespace MarsRover.Services.InputProviderSection.Exceptions
{
    public class InputNotValidException : NotValidException
    {
        public InputNotValidException(string message) : this(message, null)
        {
        }

        public InputNotValidException(string message, Exception innerEx) : base(message, innerEx)
        {
        }
    }
}