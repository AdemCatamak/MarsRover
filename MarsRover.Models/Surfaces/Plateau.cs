using MarsRover.Models.Surfaces.Exceptions;

namespace MarsRover.Models.Surfaces
{
    public class Plateau : Surface
    {
        private const int X_MIN = 0;
        private const int Y_MIN = 0;

        public int XLength { get; }
        public int YLength { get; }

        public Plateau(int xLength, int yLength)
        {
            if (xLength < 0 || yLength < 0)
            {
                throw new PlateauSizeNotValidException(xLength, yLength);
            }

            XLength = xLength;
            YLength = yLength;
        }

        public override bool Contains(Point point)
        {
            bool valid = point.X >= X_MIN && point.X <= XLength
                                             && point.Y >= Y_MIN && point.Y <= YLength;

            return valid;
        }
    }
}