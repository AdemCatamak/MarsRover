using MarsRover.Exceptions;

namespace MarsRover.Services.SurfaceBuilderSection.Exceptions
{
    public class PlateauBuilderParameterNotValidException : NotValidException
    {
        public PlateauBuilderParameterNotValidException(string message) : base(message, null)
        {
        }
    }
}