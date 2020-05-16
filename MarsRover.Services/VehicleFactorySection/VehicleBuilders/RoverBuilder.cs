using System;
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
            if (arg == null) throw new VehicleBuilderParameterNotValidException("Rover parameter should not be empty");

            string[] parameters = arg.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            if (parameters.Length != 3) throw new VehicleBuilderParameterNotValidException("Insufficient parameter for building Rover");

            string xStr = parameters[0];
            string yStr = parameters[1];
            string directionStr = parameters[2]?.ToUpperInvariant();

            if (!int.TryParse(xStr, out int x))
            {
                throw new VehicleBuilderParameterNotValidException($"Rover's 1st parameter format not valid [{arg}]");
            }
            
            if (!int.TryParse(yStr, out int y))
            {
                throw new VehicleBuilderParameterNotValidException($"Rover's 2nd parameter format not valid [{arg}]");
            }

            if (!Enum.IsDefined(typeof(CompassDirections), directionStr))
            {
                throw new VehicleBuilderParameterNotValidException($"Rover's 3th parameter format not valid [{arg}]");
            }

            var direction = (CompassDirections) Enum.Parse(typeof(CompassDirections), directionStr);

            Vehicle vehicle = new Rover(x, y, direction);
            return vehicle;
        }
    }
}