using System.IO;
using MarsRover.Services.StreamSection.Exceptions;

namespace MarsRover.Services.StreamSection.FileProcessors
{
    public class BasicFileProcessor : IFileProcessor
    {
        public bool Exists(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath))
            {
                throw new FileNameNotValidException(fullPath);
            }

            return File.Exists(fullPath);
        }
    }
}