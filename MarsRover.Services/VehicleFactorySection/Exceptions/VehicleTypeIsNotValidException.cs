using MarsRover.Exceptions;

namespace MarsRover.Services.VehicleFactorySection.Exceptions
{
    public class VehicleTypeIsNotValidException : NotValidException
    {
        public VehicleTypeIsNotValidException(string vehicleType)
            : base($"Vehicle type is not valid [{vehicleType}]", null)
        {
        }
    }
}