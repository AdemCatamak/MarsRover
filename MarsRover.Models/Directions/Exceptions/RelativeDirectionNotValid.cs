using MarsRover.Exceptions;

namespace MarsRover.Models.Directions.Exceptions
{
    public class RelativeDirectionNotValid : NotValidException
    {
        public RelativeDirectionNotValid(string relativeDirection) : base($"Relative direction is not predefined [{relativeDirection}]", null)
        {
        }
    }
}