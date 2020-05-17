using MarsRover.Models;
using MarsRover.Models.Surfaces;
using Xunit;

namespace MarsRover.ModelsTests
{
    public class Plateau_ContainsTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(0, 2)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        public void WhenAllCoordinateValue_LowerThanPlateauSize_And__GreaterThanMinSize_ContainsShouldReturnTrue(int x, int y)
        {
            var sut = new Plateau(2, 2);

            bool result = sut.Contains(new Point(x, y));

            Assert.True(result);
        }

        [Theory]
        [InlineData(3, 0)]
        [InlineData(3, 1)]
        [InlineData(3, 2)]
        [InlineData(0, 3)]
        [InlineData(1, 3)]
        [InlineData(2, 3)]
        [InlineData(3, 3)]
        public void WhenAtLeastOneCoordinateValueGreaterThanPlateauSize__ContainsShouldReturnFalse(int x, int y)
        {
            var sut = new Plateau(2, 2);

            bool result = sut.Contains(new Point(x, y));

            Assert.False(result);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(-1, 1)]
        [InlineData(-1, 2)]
        [InlineData(0, -1)]
        [InlineData(1, -1)]
        [InlineData(2, -1)]
        [InlineData(-1, -1)]
        public void WhenAtLeastOneCoordinateValueLowerThanMinSize__ContainsShouldReturnFalse(int x, int y)
        {
            var sut = new Plateau(2, 2);

            bool result = sut.Contains(new Point(x, y));

            Assert.False(result);
        }
    }
}