using MarsRover.Models;

namespace MarsRover.Services.VehicleFactorySection
{
    public interface IVehicleFactory
    {
        Vehicle Generate(VehicleTypes vehicleType, string arg);
    }
}