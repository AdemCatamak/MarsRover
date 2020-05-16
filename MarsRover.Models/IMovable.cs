using MarsRover.Models.Directions;

namespace MarsRover.Models
{
    public interface IMovable
    {
        void Move();
        void Turn(RelativeDirections direction);
    }
}