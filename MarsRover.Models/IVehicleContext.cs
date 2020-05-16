namespace MarsRover.Models
{
    public interface IVehicleContext
    {
        Vehicle Vehicle { get; }
        void Move(VehicleActions vehicleAction);
    }
}