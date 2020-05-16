using MarsRover.Services.InputProviderSection;
using MarsRover.Services.InputProviderSection.Exceptions;
using MarsRover.Services.InputProviderSection.InputProviders;
using Xunit;

namespace MarsRover.ServicesTests
{
    public class ConsoleInputProvider_ProvideTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WhenArgIsNull__InputNotValidExceptionOccurs(string arg)
        {
            var sut = new ConsoleInputProvider();

            Assert.Throws<InputNotValidException>(() => sut.Provide(arg));
        }

        [Theory]
        [InlineData("line1")]
        [InlineData("line1 \\n line2")]
        public void WhenArgContainsLessThan3Line__InputNotValidExceptionOccurs(string arg)
        {
            var sut = new ConsoleInputProvider();

            Assert.Throws<InputNotValidException>(() => sut.Provide(arg));
        }

        [Theory]
        [InlineData("surface info \\n vehicle1_info \\n vehicle1_commands \\n vehicle2_info")]
        [InlineData("surface info \\n vehicle1_info \\n vehicle1_commands \\n vehicle2_info \\n vehicle2_commands \\n vehicle3_info")]
        public void WhenArgDoesContainsEvenNumberOfLines__InputNotValidExceptionOccurs(string arg)
        {
            var sut = new ConsoleInputProvider();

            Assert.Throws<InputNotValidException>(() => sut.Provide(arg));
        }

        [Theory]
        [InlineData("surface info \\n vehicle1_info \\n vehicle1_commands", 1)]
        [InlineData("surface info \\n vehicle1_info \\n vehicle1_commands \\n vehicle2_info \\n vehicle2_commands", 2)]
        public void WhenArgDoesContainsOddNumberOfLines__InputNotValidExceptionOccurs(string arg, int expectedVehicleCount)
        {
            var sut = new ConsoleInputProvider();

            Input input = sut.Provide(arg);

            Assert.NotNull(input);

            Assert.Equal("surface info", input.SurfaceParameter);
            Assert.Equal(expectedVehicleCount, input.VehicleAndCommandsParameterList.Count);
        }
    }
}