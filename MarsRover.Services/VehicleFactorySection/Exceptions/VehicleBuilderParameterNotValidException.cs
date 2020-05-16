using System;
using MarsRover.Exceptions;

namespace MarsRover.Services.VehicleFactorySection.Exceptions
{
    public class VehicleBuilderParameterNotValidException : NotValidException
    {
        public VehicleBuilderParameterNotValidException(string message) : this(message, null)
        {
        }

        public VehicleBuilderParameterNotValidException(string message, Exception innerEx) : base(message, innerEx)
        {
        }
    }
}