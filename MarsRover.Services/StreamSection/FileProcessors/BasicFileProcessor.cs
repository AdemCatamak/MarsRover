using System.IO;
using MarsRover.Services.StreamSection.Exceptions;

namespace MarsRover.Services.StreamSection.FileProcessors
{
    public class BasicFileProcessor : IFileProcessor
    {
        public bool Exists(string fullPath)
        {
            fullPath = fullPath?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(fullPath))
            {
                throw new FileNameEmptyException();
            }

            return File.Exists(fullPath);
        }
    }
}