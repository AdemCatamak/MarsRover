using System;
using MarsRover.Exceptions;
using MarsRover.Models.Directions;
using MarsRover.Models.Directions.Exceptions;
using MarsRover.Models.Vehicles;

namespace MarsRover.Models
{
    public abstract class Vehicle : IMovable
    {
        public Point CurrentPoint { get; protected set; }
        public CompassDirections Facade { get; protected set; }

        protected Vehicle(int x, int y, CompassDirections facade)
        {
            if (!Enum.IsDefined(typeof(CompassDirections), facade))
            {
                throw new CompassDirectionNotValid(facade.ToString());
            }

            CurrentPoint = new Point(x, y);
            Facade = facade;
        }

        public override string ToString()
        {
            return $"{CurrentPoint} {Facade.ToString()}";
        }

        public void GoForward()
        {
            switch (Facade)
            {
                case CompassDirections.N:
                    CurrentPoint = new Point(CurrentPoint.X, CurrentPoint.Y + 1);
                    break;
                case CompassDirections.S:
                    CurrentPoint = new Point(CurrentPoint.X, CurrentPoint.Y - 1);
                    break;
                case CompassDirections.E:
                    CurrentPoint = new Point(CurrentPoint.X + 1, CurrentPoint.Y);
                    break;
                case CompassDirections.W:
                    CurrentPoint = new Point(CurrentPoint.X - 1, CurrentPoint.Y);
                    break;
                default:
                    throw new DevelopmentException($"{nameof(Rover)}.{nameof(Facade)} is not valid [{Facade}]");
            }
        }

        public void Turn(RelativeDirections direction)
        {
            if (!Enum.IsDefined(typeof(RelativeDirections), direction))
            {
                throw new RelativeDirectionNotValid(direction.ToString());
            }

            CompassDirections compassDirection;
            switch (direction)
            {
                case RelativeDirections.Left:
                    compassDirection = (CompassDirections) (((int) Facade + 90) % 360);
                    break;
                case RelativeDirections.Right:
                    compassDirection = (CompassDirections) (((int) Facade + 270) % 360);
                    break;
                default:
                    throw new DevelopmentException($"There is uncovered switch-case state [{direction.ToString()}]");
            }

            Facade = compassDirection;
        }
    }
}