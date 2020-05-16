namespace MarsRover.Models.VehicleContexts
{
    public class RoverContext : VehicleContext, IRoverContext
    {
        public RoverContext(Surface surface, Vehicle vehicle) : base(surface, vehicle)
        {
        }
    }
}