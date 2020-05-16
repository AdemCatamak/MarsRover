using System;
using MarsRover.Exceptions;

namespace MarsRover.Services.StreamSection.Exceptions
{
    public class FileNameNotValidException : NotValidException
    {
        public FileNameNotValidException(string fileName) : this($"Filename is not valid [supplied file name : {fileName}]", null)
        {
        }

        public FileNameNotValidException(string message, Exception innerEx) : base(message, innerEx)
        {
        }
    }
}