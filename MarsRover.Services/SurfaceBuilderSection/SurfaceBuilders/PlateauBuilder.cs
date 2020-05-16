using System;
using MarsRover.Models;
using MarsRover.Models.Surfaces;
using MarsRover.Services.SurfaceBuilderSection.Exceptions;

namespace MarsRover.Services.SurfaceBuilderSection.SurfaceBuilders
{
    public class PlateauBuilder : IPlateauBuilder
    {
        public Surface Build(string arg)
        {
            if (arg == null) throw new PlateauBuilderParameterNotValidException("Plateau parameter should not be empty");

            string[] parameters = arg.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            if (parameters.Length != 2) throw new PlateauBuilderParameterNotValidException($"Insufficient parameter count for modelling Plateau. [parameter count: {parameters.Length} - {nameof(arg)}:{arg}]");

            if (!int.TryParse(parameters[0], out int x))
            {
                throw new PlateauBuilderParameterNotValidException($"Parameters are in an invalid format for modelling Plateau [{arg}]");
            }

            if (!int.TryParse(parameters[1], out int y))
            {
                throw new PlateauBuilderParameterNotValidException($"Parameters are in an invalid format for modelling Plateau [{arg}]");
            }

            Surface surface = new Plateau(x, y);
            return surface;
        }
    }
}