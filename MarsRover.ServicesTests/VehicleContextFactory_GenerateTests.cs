using MarsRover.Exceptions;
using MarsRover.Models;
using MarsRover.Models.Directions;
using MarsRover.Models.VehicleContexts;
using MarsRover.Models.VehicleContexts.Exceptions;
using MarsRover.Models.Vehicles;
using MarsRover.Services.VehicleContextFactorySection.VehicleContextFactories;
using Moq;
using Xunit;

namespace MarsRover.ServicesTests
{
    public class VehicleContextFactory_GenerateTests
    {
        private readonly Mock<Surface> _surfaceMock;

        public VehicleContextFactory_GenerateTests()
        {
            _surfaceMock = new Mock<Surface>();
            _surfaceMock.Setup(surface => surface.Contains(It.IsAny<Point>()))
                        .Returns(true);
        }

        [Fact]
        public void WhenSurfaceIsNull__DevelopmentExceptionOccurs()
        {
            var vehicleMock = new Mock<Vehicle>(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CompassDirections>());

            var sut = new VehicleContextFactory();

            Assert.Throws<DevelopmentException>(() => sut.Generate(null, vehicleMock.Object));
        }

        [Fact]
        public void WhenVehicleIsNull__DevelopmentExceptionOccurs()
        {
            var sut = new VehicleContextFactory();

            Assert.Throws<DevelopmentException>(() => sut.Generate(_surfaceMock.Object, null));
        }


        [Fact]
        public void WhenThereIsNoVehicleContextForVehicle__DevelopmentExceptionOccurs()
        {
            var vehicleMock = new Mock<Vehicle>(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CompassDirections>());

            var sut = new VehicleContextFactory();

            Assert.Throws<DevelopmentException>(() => sut.Generate(_surfaceMock.Object, vehicleMock.Object));
        }

        [Fact]
        public void WhenThereIsVehicleContextForVehicle_But_DeployPointNotOnSurface__VehicleDeploymentExceptionOccurs()
        {
            var vehicleMock = new Mock<Rover>(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CompassDirections>());
            _surfaceMock.Setup(surface => surface.Contains(It.IsAny<Point>()))
                        .Returns(false);

            var sut = new VehicleContextFactory();

            Assert.Throws<VehicleDeployException>(() => sut.Generate(_surfaceMock.Object, vehicleMock.Object));
        }

        [Fact]
        public void WhenThereIsVehicleContextForVehicle_And_DeployPointOnSurface__VehicleContextShouldNotBeNull()
        {
            var vehicleMock = new Mock<Rover>(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CompassDirections>());

            var sut = new VehicleContextFactory();

            IVehicleContext vehicleContext = sut.Generate(_surfaceMock.Object, vehicleMock.Object);

            Assert.NotNull(vehicleContext);

            Assert.True(vehicleContext is IRoverContext);
        }
    }
}