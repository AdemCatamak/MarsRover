using System;
using System.Linq;
using MarsRover.Models;
using MarsRover.Models.Directions;
using MarsRover.Models.Vehicles;
using MarsRover.Services.VehicleFactorySection.Exceptions;

namespace MarsRover.Services.VehicleFactorySection.VehicleBuilders
{
    public class RoverBuilder : IRoverBuilder
    {
        public Vehicle Build(string arg)
        {
            if (arg == null) throw new VehicleBuilderParameterNotValidException("Parameter should not be empty");

            string[] parameters = arg.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            if (parameters.Length != 3) throw new VehicleBuilderParameterNotValidException("Insufficient parameter count");

            string xStr = parameters[0];
            string yStr = parameters[1];
            string directionStr = parameters[2]?.ToUpperInvariant();

            if (!xStr.All(char.IsDigit))
            {
                throw new VehicleBuilderParameterNotValidException("1st parameter format not valid");
            }

            if (!yStr.All(char.IsDigit))
            {
                throw new VehicleBuilderParameterNotValidException("2nd parameter format not valid");
            }

            if (!Enum.IsDefined(typeof(CompassDirections), directionStr))
            {
                throw new VehicleBuilderParameterNotValidException("3th parameter format not valid");
            }

            int x = Convert.ToInt32(xStr);
            int y = Convert.ToInt32(yStr);
            var direction = (CompassDirections) Enum.Parse(typeof(CompassDirections), directionStr);

            Vehicle vehicle = new Rover(x, y, direction);
            return vehicle;
        }
    }
}