using MarsRover.Models.Surfaces.Exceptions;

namespace MarsRover.Models.Surfaces
{
    public class Plateau : Surface
    {
        private const int X_MIN = 0;
        private const int Y_MIN = 0;

        public Plateau(int xLength, int yLength) : base(xLength, yLength)
        {
            if (XLength < 0 || YLength < 0)
            {
                throw new PlateauSizeNotValidException(XLength, YLength);
            }
        }

        public override bool Contains(Position position)
        {
            bool valid = !(position.X < X_MIN || position.X > XLength
                                             || position.Y < Y_MIN || position.Y > YLength);
            return valid;
        }
    }
}