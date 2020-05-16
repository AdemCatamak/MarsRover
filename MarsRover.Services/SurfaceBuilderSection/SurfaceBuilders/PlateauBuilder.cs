using System;
using System.Linq;
using MarsRover.Models;
using MarsRover.Models.Surfaces;
using MarsRover.Services.SurfaceBuilderSection.Exceptions;

namespace MarsRover.Services.SurfaceBuilderSection.SurfaceBuilders
{
    public class PlateauBuilder : IPlateauBuilder
    {
        public Surface Build(string arg)
        {
            if (arg == null) throw new PlateauBuilderParameterNotValidException("Parameter should not be empty");

            string[] parameters = arg.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            if (parameters.Length != 2) throw new PlateauBuilderParameterNotValidException($"Parameter count not valid [parameter count: {parameters.Length} - {nameof(arg)}:{arg}]");

            if (!parameters.All(p => p.All(char.IsDigit)))
            {
                throw new PlateauBuilderParameterNotValidException($"Parameter format not valid [{arg}]");
            }

            int x = Convert.ToInt32(parameters[0]);
            int y = Convert.ToInt32(parameters[1]);

            Surface surface = new Plateau(x, y);
            return surface;
        }
    }
}