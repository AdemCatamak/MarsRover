using System.Collections.Generic;
using System.Linq;
using MarsRover.Exceptions;
using MarsRover.Models;
using MarsRover.Services.VehicleFactorySection.Exceptions;
using MarsRover.Services.VehicleFactorySection.VehicleBuilders;

namespace MarsRover.Services.VehicleFactorySection.VehicleFactories
{
    public class VehicleFactory : IVehicleFactory
    {
        private readonly IEnumerable<IVehicleBuilder> _vehicleBuilders;

        public VehicleFactory(IEnumerable<IVehicleBuilder> vehicleBuilders)
        {
            _vehicleBuilders = vehicleBuilders ?? throw new DevelopmentException($"{nameof(vehicleBuilders)} should not be null");
        }

        public Vehicle Generate(VehicleTypes vehicleType, string arg)
        {
            IVehicleBuilder vehicleBuilder;
            switch (vehicleType)
            {
                case VehicleTypes.Rover:
                    vehicleBuilder = _vehicleBuilders.FirstOrDefault(v => v is IRoverBuilder);
                    break;
                default:
                    throw new VehicleTypeIsNotValidException(vehicleType.ToString());
            }

            if (vehicleBuilder == null)
            {
                throw new DevelopmentException($"{nameof(vehicleBuilder)} should not be null");
            }

            Vehicle movable = vehicleBuilder.Build(arg);
            return movable;
        }
    }
}