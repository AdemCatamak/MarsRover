using MarsRover.Models.Surfaces;
using MarsRover.Models.Surfaces.Exceptions;
using Xunit;

namespace MarsRover.ModelsTests
{
    public class Plateau_ConstructorTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        [InlineData(4, 2)]
        public void WhenParametersAreNotNegative__ObjectShouldBeCreated(int xLength, int yLength)
        {
            var sut = new Plateau(xLength, yLength);

            Assert.NotNull(sut);
        }

        [Theory]
        [InlineData(0, -1)]
        [InlineData(-1, 0)]
        [InlineData(-1, -1)]
        [InlineData(-4, -2)]
        public void WhenAtLeastOneParameterIsNegative__PlateauSizeNotValidExceptionOccurs(int xLength, int yLength)
        {
            Assert.Throws<PlateauSizeNotValidException>(() => new Plateau(xLength, yLength));
        }
    }
}