using System;
using MarsRover.Exceptions;

namespace MarsRover.Services.InputProviderSection.Exceptions
{
    public class InputProviderNotValidException : NotValidException
    {
        public InputProviderNotValidException(string inputProviderType) :
            this($"Input provider type is not valid [{inputProviderType}]", null)
        {
        }

        public InputProviderNotValidException(string message, Exception innerEx) : base(message, innerEx)
        {
        }
    }
}