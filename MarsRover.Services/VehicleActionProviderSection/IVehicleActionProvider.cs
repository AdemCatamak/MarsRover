using System.Collections.Generic;
using MarsRover.Models;

namespace MarsRover.Services.VehicleActionProviderSection
{
    public interface IVehicleActionProvider
    {
        IEnumerable<VehicleActions> Provide(string arg);
    }
}