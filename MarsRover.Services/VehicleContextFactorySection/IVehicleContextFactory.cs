using MarsRover.Models;

namespace MarsRover.Services.VehicleContextFactorySection
{
    public interface IVehicleContextFactory
    {
        IVehicleContext Generate(Surface surface, Vehicle vehicle);
    }
}