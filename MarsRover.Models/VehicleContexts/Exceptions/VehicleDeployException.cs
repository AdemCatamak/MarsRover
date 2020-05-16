using System;

namespace MarsRover.Models.VehicleContexts.Exceptions
{
    public class VehicleDeployException : Exception
    {
        public VehicleDeployException(Exception innerEx = null)
            : this("Vehicle deploy point not on surface", innerEx)
        {
        }

        public VehicleDeployException(string message, Exception innerEx = null)
            : base(message, innerEx)
        {
        }
    }
}