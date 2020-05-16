using MarsRover.Exceptions;

namespace MarsRover.Models.VehicleContexts.Exceptions
{
    public class VehicleActionIsNotValidException : NotValidException
    {
        public VehicleActionIsNotValidException(string vehicleAction)
            : base($"Vehicle command contains invalid character [{vehicleAction}]", null)
        {
        }
    }
}