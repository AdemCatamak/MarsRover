using MarsRover.Exceptions;

namespace MarsRover.Services.StreamSection.Exceptions
{
    public class FileNameEmptyException : NotValidException
    {
        public FileNameEmptyException() : base($"File name should not be empty", null)
        {
        }
    }
}