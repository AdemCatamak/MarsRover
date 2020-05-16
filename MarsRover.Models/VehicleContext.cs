using System;
using MarsRover.Exceptions;
using MarsRover.Models.Directions;
using MarsRover.Models.VehicleContexts.Exceptions;

namespace MarsRover.Models
{
    public abstract class VehicleContext : IVehicleContext
    {
        public Vehicle Vehicle { get; }
        private Surface Surface { get; }

        public VehicleContext(Surface surface, Vehicle vehicle)
        {
            Surface = surface;
            Vehicle = vehicle;

            bool positionIsInSurface = Surface.Contains(Vehicle.CurrentPosition);
            if (!positionIsInSurface) throw new VehicleDeployException();
        }

        public void Move(VehicleActions vehicleAction)
        {
            if (!Enum.IsDefined(typeof(VehicleActions), vehicleAction))
            {
                throw new VehicleActionIsNotValidException(vehicleAction.ToString());
            }

            switch (vehicleAction)
            {
                case VehicleActions.L:
                    Vehicle.Turn(RelativeDirections.Left);
                    break;
                case VehicleActions.R:
                    Vehicle.Turn(RelativeDirections.Right);
                    break;
                case VehicleActions.M:
                    Vehicle.Move();
                    bool positionIsInSurface = Surface.Contains(Vehicle.CurrentPosition);
                    if (!positionIsInSurface) throw new VehicleConnectionLostException();
                    break;
                default:
                    throw new DevelopmentException($"There is uncovered switch-case state [{vehicleAction.ToString()}]");
            }
        }
    }
}