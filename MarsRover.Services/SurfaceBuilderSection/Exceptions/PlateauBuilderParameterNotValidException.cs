using System;
using MarsRover.Exceptions;

namespace MarsRover.Services.SurfaceBuilderSection.Exceptions
{
    public class PlateauBuilderParameterNotValidException : NotValidException
    {
        public PlateauBuilderParameterNotValidException(string message) : this(message, null)
        {
        }

        public PlateauBuilderParameterNotValidException(string message, Exception innerEx) : base(message, innerEx)
        {
        }
    }
}