using System;
using MarsRover.Exceptions;

namespace MarsRover.Models.VehicleContexts.Exceptions
{
    public class VehicleActionIsNotValidException : NotValidException
    {
        public VehicleActionIsNotValidException(string vehicleAction)
            : this($"Vehicle action is not valid [{vehicleAction}]", null)
        {
        }

        public VehicleActionIsNotValidException(string message, Exception innerEx) : base(message, innerEx)
        {
        }
    }
}