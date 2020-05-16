using System;
using System.Collections.Generic;
using MarsRover.Models;
using MarsRover.Models.VehicleContexts.Exceptions;

namespace MarsRover.Services.VehicleActionProviderSection.VehicleActionProviders
{
    public class BulkStringVehicleActionProvider : IVehicleActionProvider
    {
        public IEnumerable<VehicleActions> Provide(string arg)
        {
            arg = arg ?? string.Empty;
            var vehicleActions = new List<VehicleActions>();
            foreach (char c in arg)
            {
                if (Enum.TryParse(c.ToString().ToUpperInvariant(), out VehicleActions action))
                {
                    vehicleActions.Add(action);
                }
                else
                {
                    throw new VehicleActionIsNotValidException($"{arg} contains not valid character");
                }
            }

            return vehicleActions;
        }
    }
}