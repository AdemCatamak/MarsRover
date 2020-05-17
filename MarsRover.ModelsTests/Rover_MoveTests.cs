using MarsRover.Models;
using MarsRover.Models.Directions;
using MarsRover.Models.Vehicles;
using Xunit;

namespace MarsRover.ModelsTests
{
    public class Rover_MoveTests
    {
        [Fact]
        public void WhenMoveOperationExecuted_CurrentPositionShouldNotBeSame()
        {
            var sut = new Rover();
            var initialPosition = new Point(sut.CurrentPoint.X, sut.CurrentPoint.Y);

            sut.GoForward();

            Assert.NotEqual(initialPosition, sut.CurrentPoint);
        }

        [Theory]
        [InlineData(0, 0, CompassDirections.N, 0, 1)]
        [InlineData(0, 0, CompassDirections.S, 0, -1)]
        [InlineData(0, 0, CompassDirections.W, -1, 0)]
        [InlineData(0, 0, CompassDirections.E, 1, 0)]
        [InlineData(4, 2, CompassDirections.N, 4, 3)]
        [InlineData(4, 2, CompassDirections.S, 4, 1)]
        [InlineData(4, 2, CompassDirections.W, 3, 2)]
        [InlineData(4, 2, CompassDirections.E, 5, 2)]
        public void WhenMoveOperationExecuted_RoverStateShouldBeChange(int currentX, int currentY, CompassDirections currentDirection,
                                                                       int expectedX, int expectedY)
        {
            var sut = new Rover(currentX, currentY, currentDirection);
            var expectedPosition = new Point(expectedX, expectedY);

            sut.GoForward();

            Assert.Equal(expectedPosition, sut.CurrentPoint);
            Assert.Equal(currentDirection, sut.Facade);
        }
    }
}