using System.Collections.Generic;
using MarsRover.Exceptions;
using MarsRover.Services.VehicleFactorySection;
using MarsRover.Services.VehicleFactorySection.Exceptions;
using MarsRover.Services.VehicleFactorySection.VehicleBuilders;
using MarsRover.Services.VehicleFactorySection.VehicleFactories;
using Moq;
using Xunit;

namespace MarsRover.ServicesTests
{
    public class VehicleFactory_GenerateTests
    {
        [Fact]
        public void WhenVehicleBuildersIsNull_DevelopmentExceptionOccurs()
        {
            Assert.Throws<DevelopmentException>(() => new VehicleFactory(null));
        }

        [Fact]
        public void WhenVehicleBuilderCollectionDoesNotContainsAnyItem__DevelopmentExceptionOccurs()
        {
            var sut = new VehicleFactory(new List<IVehicleBuilder>());

            Assert.Throws<DevelopmentException>(() => sut.Generate(VehicleTypes.Rover, It.IsAny<string>()));
        }

        [Fact]
        public void WhenVehicleTypeIsNotValid__VehicleTypeIsNotValidExceptionOccurs()
        {
            var sut = new VehicleFactory(new List<IVehicleBuilder>());

            Assert.Throws<VehicleTypeIsNotValidException>(() => sut.Generate(0, It.IsAny<string>()));
        }

        [Fact]
        public void WhenParametersAreValid_And_BuilderIsFound__BuildMethodShouldBeInvoked()
        {
            var roverBuilderMock = new Mock<IRoverBuilder>();
            var sut = new VehicleFactory(new List<IVehicleBuilder> {roverBuilderMock.Object});

            sut.Generate(VehicleTypes.Rover, It.IsAny<string>());

            roverBuilderMock.Verify(builder => builder.Build(It.IsAny<string>()), Times.Once);
        }
    }
}