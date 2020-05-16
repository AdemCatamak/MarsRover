using System;
using MarsRover.Exceptions;

namespace MarsRover.Models.Directions.Exceptions
{
    public class CompassDirectionNotValid : NotValidException
    {
        public CompassDirectionNotValid(string compassDirection) : base($"Compass direction is not predefined [{compassDirection}]", null)
        {
        }

        public CompassDirectionNotValid(string message, Exception innerEx) : base(message, innerEx)
        {
        }
    }
}