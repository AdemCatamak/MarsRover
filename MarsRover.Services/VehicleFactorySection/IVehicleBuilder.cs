using MarsRover.Models;

namespace MarsRover.Services.VehicleFactorySection
{
    public interface IVehicleBuilder
    {
        Vehicle Build(string arg);
    }
}