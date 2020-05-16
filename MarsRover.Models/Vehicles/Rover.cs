using MarsRover.Models.Directions;

namespace MarsRover.Models.Vehicles
{
    public class Rover : Vehicle
    {
        public Rover() : this(0, 0, CompassDirections.N)
        {
        }

        public Rover(int x, int y, CompassDirections facade) : base(x, y, facade)
        {
        }
    }
}