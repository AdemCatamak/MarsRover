using System;
using System.Linq;
using MarsRover.Services.InputProviderSection.Exceptions;

namespace MarsRover.Services.InputProviderSection.InputProviders
{
    public class ConsoleInputProvider : IConsoleInputProvider
    {
        public Input Provide(string arg)
        {
            arg = arg ?? string.Empty;
            string[] lines = arg.Split(new[] {"\\n", "|"}, StringSplitOptions.RemoveEmptyEntries)
                                .Select(line => line.Trim())
                                .Where(line => !string.IsNullOrEmpty(line))
                                .ToArray()
                ;
            if (lines == null || lines.Length == 0)
            {
                throw new InputNotValidException("Input should not be null or empty");
            }

            if (lines.Length < 3)
            {
                throw new InputNotValidException("At least 3 lines should be supplied");
            }

            if (lines.Length % 2 != 1)
            {
                throw new InputNotValidException("Each vehicle should has at least one action command");
            }

            var input = new Input
                        {
                            SurfaceParameter = lines[0]?.Trim()
                        };

            for (var i = 1; i < lines.Length; i += 2)
            {
                var vehicleAndCommands = new Tuple<string, string>(lines[i]?.Trim(), lines[i + 1]?.Trim());
                input.VehicleAndCommandsParameterList.Add(vehicleAndCommands);
            }

            return input;
        }
    }
}