using System;
using MarsRover.Exceptions;

namespace MarsRover.Services.VehicleFactorySection.Exceptions
{
    public class VehicleTypeIsNotValidException : NotValidException
    {
        public VehicleTypeIsNotValidException(string vehicleType)
            : this($"Vehicle type is not valid [{vehicleType}]", null)
        {
        }

        public VehicleTypeIsNotValidException(string message, Exception innerEx) : base(message, innerEx)
        {
        }
    }
}