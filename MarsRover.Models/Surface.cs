namespace MarsRover.Models
{
    public abstract class Surface
    {
        public int XLength { get; }
        public int YLength { get; }

        protected Surface(int xLength, int yLength)
        {
            XLength = xLength;
            YLength = yLength;
        }

        public abstract bool Contains(Position position);
    }
}