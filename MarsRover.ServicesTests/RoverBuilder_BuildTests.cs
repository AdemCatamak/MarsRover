using MarsRover.Models;
using MarsRover.Models.Directions;
using MarsRover.Services.VehicleFactorySection.Exceptions;
using MarsRover.Services.VehicleFactorySection.VehicleBuilders;
using Xunit;

namespace MarsRover.ServicesTests
{
    public class RoverBuilder_BuildTests
    {
        [Fact]
        public void WhenParameterIsNull__VehicleBuilderParameterNotValidExceptionOccurs()
        {
            var sut = new RoverBuilder();

            Assert.Throws<VehicleBuilderParameterNotValidException>(() => sut.Build(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("1 2")]
        [InlineData(" 1 2")]
        [InlineData("1 2 ")]
        [InlineData(" 1 2 ")]
        [InlineData(" 1 2 3 4")]
        public void WhenParameterCountIsNot3__VehicleBuilderParameterNotValidExceptionOccurs(string arg)
        {
            var sut = new RoverBuilder();

            Assert.Throws<VehicleBuilderParameterNotValidException>(() => sut.Build(arg));
        }

        [Theory]
        [InlineData("a 0 N")]
        [InlineData("0 a N")]
        [InlineData("a a N")]
        [InlineData("1 2 0")]
        public void WhenParameterIsNotValid__VehicleBuilderParameterNotValidExceptionOccurs(string arg)
        {
            var sut = new RoverBuilder();

            Assert.Throws<VehicleBuilderParameterNotValidException>(() => sut.Build(arg));
        }

        [Theory]
        [InlineData("0 0 N", 0, 0, CompassDirections.N)]
        [InlineData("1 1 S", 1, 1, CompassDirections.S)]
        public void WhenParameterCountIs3_And_ParametersAreValid__ResponseShouldNotBeNull(string arg,
                                                                                          int expectedX, int expectedY, CompassDirections expectedDirection)
        {
            var sut = new RoverBuilder();

            Vehicle vehicle = sut.Build(arg);

            Assert.NotNull(vehicle);

            Assert.Equal(expectedX, vehicle.CurrentPosition.X);
            Assert.Equal(expectedY, vehicle.CurrentPosition.Y);
            Assert.Equal(expectedDirection, vehicle.Facade);
        }
    }
}