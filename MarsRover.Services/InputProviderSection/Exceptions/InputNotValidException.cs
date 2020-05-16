using MarsRover.Exceptions;

namespace MarsRover.Services.InputProviderSection.Exceptions
{
    public class InputNotValidException : NotValidException
    {
        public InputNotValidException(string message) : base(message, null)
        {
        }
    }
}