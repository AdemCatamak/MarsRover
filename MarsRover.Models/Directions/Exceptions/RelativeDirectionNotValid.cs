using System;
using MarsRover.Exceptions;

namespace MarsRover.Models.Directions.Exceptions
{
    public class RelativeDirectionNotValid : NotValidException
    {
        public RelativeDirectionNotValid(string relativeDirection) : this($"Relative direction is not predefined [{relativeDirection}]", null)
        {
        }

        public RelativeDirectionNotValid(string message, Exception innerEx) : base(message, innerEx)
        {
        }
    }
}