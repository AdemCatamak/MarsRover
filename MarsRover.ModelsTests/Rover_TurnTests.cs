using MarsRover.Models;
using MarsRover.Models.Directions;
using MarsRover.Models.Directions.Exceptions;
using MarsRover.Models.Vehicles;
using Xunit;

namespace MarsRover.ModelsTests
{
    public class Rover_TurnTests
    {
        [Fact]
        public void WhenTurnOperationExecutedWithInvalidRelativeDirection_RelativeDirectionNotValidExceptionOccurs()
        {
            var sut = new Rover();

            Assert.Throws<RelativeDirectionNotValid>(() => sut.Turn((RelativeDirections) 100));
        }

        [Theory]
        [InlineData(RelativeDirections.Left)]
        [InlineData(RelativeDirections.Right)]
        public void WhenTurnOperationExecuted_CurrentPositionShouldBeSame(RelativeDirections relativeDirection)
        {
            var sut = new Rover();
            var initialPosition = new Point(sut.CurrentPoint.X, sut.CurrentPoint.Y);

            sut.Turn(relativeDirection);

            Assert.Equal(initialPosition, sut.CurrentPoint);
        }

        [Theory]
        [InlineData(0, 0, CompassDirections.N, RelativeDirections.Left, CompassDirections.W)]
        [InlineData(0, 0, CompassDirections.N, RelativeDirections.Right, CompassDirections.E)]
        [InlineData(0, 0, CompassDirections.W, RelativeDirections.Left, CompassDirections.S)]
        [InlineData(0, 0, CompassDirections.W, RelativeDirections.Right, CompassDirections.N)]
        [InlineData(0, 0, CompassDirections.S, RelativeDirections.Left, CompassDirections.E)]
        [InlineData(0, 0, CompassDirections.S, RelativeDirections.Right, CompassDirections.W)]
        [InlineData(0, 0, CompassDirections.E, RelativeDirections.Left, CompassDirections.N)]
        [InlineData(0, 0, CompassDirections.E, RelativeDirections.Right, CompassDirections.S)]
        public void WhenMoveOperationExecuted_RoverStateShouldBeChange(int currentX, int currentY, CompassDirections currentDirection,
                                                                       RelativeDirections turn, CompassDirections expecedDirection)
        {
            var sut = new Rover(currentX, currentY, currentDirection);
            var expectedPosition = new Point(currentX, currentY);

            sut.Turn(turn);

            Assert.Equal(expectedPosition, sut.CurrentPoint);
            Assert.Equal(expecedDirection, sut.Facade);
        }
    }
}