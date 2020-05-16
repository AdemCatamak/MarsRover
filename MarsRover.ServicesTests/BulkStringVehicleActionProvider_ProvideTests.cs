using System.Collections.Generic;
using MarsRover.Models;
using MarsRover.Models.VehicleContexts.Exceptions;
using MarsRover.Services.VehicleActionProviderSection.VehicleActionProviders;
using Xunit;

namespace MarsRover.ServicesTests
{
    public class BulkStringVehicleActionProvider_ProvideTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WhenStringDoesNotContainsAnyCharacter__ResponseHasNoAction(string arg)
        {
            var sut = new BulkStringVehicleActionProvider();

            IEnumerable<VehicleActions> vehicleActions = sut.Provide(arg);

            Assert.NotNull(vehicleActions);
            Assert.Empty(vehicleActions);
        }

        [Fact]
        public void WhenStringContainsInvalidVehicleAction__VehicleActionIsNotValidExceptionOccurs()
        {
            var sut = new BulkStringVehicleActionProvider();

            Assert.Throws<VehicleActionIsNotValidException>(() => sut.Provide("-"));
        }

        [Theory]
        [InlineData("M", VehicleActions.M)]
        [InlineData("L", VehicleActions.L)]
        [InlineData("R", VehicleActions.R)]
        [InlineData("MLR", VehicleActions.M, VehicleActions.L, VehicleActions.R)]
        public void WhenStringContainsValidVehicleAction__ResponseContainsAllActions(string arg, params VehicleActions[] expectedVehicleActions)
        {
            var sut = new BulkStringVehicleActionProvider();

            IEnumerable<VehicleActions> vehicleActions = sut.Provide(arg);

            Assert.NotNull(vehicleActions);
            Assert.Equal(expectedVehicleActions, vehicleActions);
        }
    }
}