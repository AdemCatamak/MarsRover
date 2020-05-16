using System;
using MarsRover.Exceptions;
using MarsRover.Models.Directions;
using MarsRover.Models.Directions.Exceptions;
using MarsRover.Models.Vehicles;

namespace MarsRover.Models
{
    public abstract class Vehicle : IMovable
    {
        public Position CurrentPosition { get; protected set; }
        public CompassDirections Facade { get; protected set; }

        protected Vehicle(int x, int y, CompassDirections facade)
        {
            if (!Enum.IsDefined(typeof(CompassDirections), facade))
            {
                throw new CompassDirectionNotValid(facade.ToString());
            }

            CurrentPosition = new Position(x, y);
            Facade = facade;
        }

        public override string ToString()
        {
            return $"{CurrentPosition} {Facade.ToString()}";
        }

        public void Move()
        {
            switch (Facade)
            {
                case CompassDirections.N:
                    CurrentPosition = new Position(CurrentPosition.X, CurrentPosition.Y + 1);
                    break;
                case CompassDirections.S:
                    CurrentPosition = new Position(CurrentPosition.X, CurrentPosition.Y - 1);
                    break;
                case CompassDirections.E:
                    CurrentPosition = new Position(CurrentPosition.X + 1, CurrentPosition.Y);
                    break;
                case CompassDirections.W:
                    CurrentPosition = new Position(CurrentPosition.X - 1, CurrentPosition.Y);
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

            switch (direction)
            {
                case RelativeDirections.Left:
                    Facade = (CompassDirections) (((int) Facade + 90) % 360);
                    break;
                case RelativeDirections.Right:
                    Facade = (CompassDirections) (((int) Facade + 270) % 360);
                    break;
                default:
                    throw new DevelopmentException($"There is uncovered switch-case state [{direction.ToString()}]");
            }
        }
    }
}