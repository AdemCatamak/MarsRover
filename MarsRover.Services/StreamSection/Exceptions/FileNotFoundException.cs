using MarsRover.Exceptions;

namespace MarsRover.Services.StreamSection.Exceptions
{
    public class FileNotFoundException : NotFoundException
    {
        public FileNotFoundException(string fileName) : base($"File could not found with given name [{fileName}]", null)
        {
        }
    }
}