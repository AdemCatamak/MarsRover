using MarsRover.Exceptions;

namespace MarsRover.Services.InputProviderSection.Exceptions
{
    public class InputProviderNotValidException : NotValidException
    {
        public InputProviderNotValidException(string inputProviderType) :
            base($"Input provider type is not valid [{inputProviderType}]", null)
        {
        }
    }
}