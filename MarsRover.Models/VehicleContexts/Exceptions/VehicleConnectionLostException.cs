using System;

namespace MarsRover.Models.VehicleContexts.Exceptions
{
    public class VehicleConnectionLostException : Exception
    {
        public VehicleConnectionLostException(Exception innerEx = null)
            : this("Vehicle Connection Lost", innerEx)
        {
        }

        public VehicleConnectionLostException(string message, Exception innerEx = null)
            : base(message, innerEx)
        {
        }
    }
}