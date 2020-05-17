using MarsRover.Models;
using MarsRover.Models.Surfaces;
using MarsRover.Services.SurfaceBuilderSection.Exceptions;
using MarsRover.Services.SurfaceBuilderSection.SurfaceBuilders;
using Xunit;

namespace MarsRover.ServicesTests
{
    public class PlateauBuilder_BuildTests
    {
        [Fact]
        public void WhenParameterIsNull__PlateauBuilderParameterNotValidExceptionOccurs()
        {
            var sut = new PlateauBuilder();

            Assert.Throws<PlateauBuilderParameterNotValidException>(() => sut.Build(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData("1")]
        [InlineData(" 1")]
        [InlineData(" 1 ")]
        [InlineData(" 1 2 3")]
        [InlineData(" 1 2 3 4")]
        public void WhenParameterCountIsNot2__PlateauBuilderParameterNotValidExceptionOccurs(string arg)
        {
            var sut = new PlateauBuilder();

            Assert.Throws<PlateauBuilderParameterNotValidException>(() => sut.Build(arg));
        }

        [Theory]
        [InlineData("a 0")]
        [InlineData("0 a")]
        [InlineData("a a")]
        public void WhenParameterIsNotValid__PlateauBuilderParameterNotValidExceptionOccurs(string arg)
        {
            var sut = new PlateauBuilder();

            Assert.Throws<PlateauBuilderParameterNotValidException>(() => sut.Build(arg));
        }

        [Theory]
        [InlineData("0 0", 0, 0)]
        [InlineData("1 1", 1, 1)]
        public void WhenParameterCountIs2_And_ParametersAreValid__ResponseShouldNotBeNull(string arg,
                                                                                          int expectedX, int expectedY)
        {
            var sut = new PlateauBuilder();

            Surface surface = sut.Build(arg);

            Assert.NotNull(surface);

            var plateau = surface as Plateau;
            Assert.NotNull(plateau);

            Assert.Equal(expectedX, plateau.XLength);
            Assert.Equal(expectedY, plateau.YLength);
        }
    }
}