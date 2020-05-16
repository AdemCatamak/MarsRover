using MarsRover.Models;
using MarsRover.Models.Directions;
using MarsRover.Models.VehicleContexts;
using MarsRover.Models.VehicleContexts.Exceptions;
using MarsRover.Models.Vehicles;
using Moq;
using Xunit;

namespace MarsRover.ModelsTests
{
    public class RoverContext_MoveTests
    {
        private Mock<Surface> _surfaceMock;

        public RoverContext_MoveTests()
        {
            _surfaceMock = new Mock<Surface>(It.IsAny<int>(), It.IsAny<int>());
            _surfaceMock.Setup(surface => surface.Contains(It.IsAny<Position>()))
                        .Returns(true);
        }

        [Theory]
        [InlineData(VehicleActions.L, CompassDirections.W)]
        [InlineData(VehicleActions.R, CompassDirections.E)]
        public void ActionIsTurn__VehicleTurnMethodWillBeInvoked(VehicleActions vehicleAction, CompassDirections expectedDirection)
        {
            const int x = 0;
            const int y = 0;
            const CompassDirections direction = CompassDirections.N;

            var rover = new Rover(x, y, direction);
            var sut = new RoverContext(_surfaceMock.Object,
                                       rover);

            sut.Move(vehicleAction);

            Assert.Equal(x, sut.Vehicle.CurrentPosition.X);
            Assert.Equal(y, sut.Vehicle.CurrentPosition.Y);
            Assert.Equal(expectedDirection, sut.Vehicle.Facade);
        }


        [Fact]
        public void ActionIsMove_And_SurfaceIsBigEnough__VehiclePositionShouldChange()
        {
            const int x = 0;
            const int y = 0;
            const CompassDirections direction = CompassDirections.N;

            var rover = new Rover(x, y, direction);
            var sut = new RoverContext(_surfaceMock.Object,
                                       rover);

            sut.Move(VehicleActions.M);

            Assert.Equal(x, sut.Vehicle.CurrentPosition.X);
            Assert.Equal(y + 1, sut.Vehicle.CurrentPosition.Y);
            Assert.Equal(direction, sut.Vehicle.Facade);
        }


        [Fact]
        public void ActionIsMove_And_SurfaceIsNotBigEnough__VehiclePositionShouldChange()
        {
            const int x = 0;
            const int y = 0;
            const CompassDirections direction = CompassDirections.N;

            var rover = new Rover(x, y, direction);
            var sut = new RoverContext(_surfaceMock.Object,
                                       rover);

            _surfaceMock.Setup(surface => surface.Contains(It.IsAny<Position>()))
                        .Returns(false);

            Assert.Throws<VehicleConnectionLostException>(() => sut.Move(VehicleActions.M));
        }
    }
}