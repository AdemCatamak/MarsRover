using MarsRover.Exceptions;

namespace MarsRover.Services.VehicleFactorySection.Exceptions
{
    public class VehicleBuilderParameterNotValidException : NotValidException
    {
        public VehicleBuilderParameterNotValidException(string message) : base(message, null)
        {
        }
    }
}