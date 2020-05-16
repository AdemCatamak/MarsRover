using MarsRover.Exceptions;
using MarsRover.Models;
using MarsRover.Models.VehicleContexts;
using MarsRover.Models.Vehicles;

namespace MarsRover.Services.VehicleContextFactorySection.VehicleContextFactories
{
    public class VehicleContextFactory : IVehicleContextFactory
    {
        public IVehicleContext Generate(Surface surface, Vehicle vehicle)
        {
            surface = surface ?? throw new DevelopmentException($"{nameof(surface)} should not be null");
            vehicle = vehicle ?? throw new DevelopmentException($"{nameof(vehicle)} should not be null");

            IVehicleContext vehicleContext;
            if (vehicle is Rover)
                vehicleContext = new RoverContext(surface, vehicle);
            else
            {
                throw new DevelopmentException($"There is no predefined VehicleContext for Vehicle [{vehicle.GetType().Name}]");
            }

            return vehicleContext;
        }
    }
}