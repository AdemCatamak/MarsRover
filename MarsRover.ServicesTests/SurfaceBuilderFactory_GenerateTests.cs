using System.Collections.Generic;
using MarsRover.Exceptions;
using MarsRover.Models.Surfaces;
using MarsRover.Services.SurfaceBuilderSection;
using MarsRover.Services.SurfaceBuilderSection.SurfaceBuilderFactories;
using MarsRover.Services.SurfaceBuilderSection.SurfaceBuilders;
using Moq;
using Xunit;

namespace MarsRover.ServicesTests
{
    public class SurfaceBuilderFactory_GenerateTests
    {
        [Fact]
        public void WhenSurfaceBuildersIsNull_DevelopmentExceptionOccurs()
        {
            Assert.Throws<DevelopmentException>(() => new SurfaceBuilderFactory(null));
        }

        [Fact]
        public void WhenSurfaceBuilderCollectionDoesNotContainsAnyItem__DevelopmentExceptionOccurs()
        {
            var sut = new SurfaceBuilderFactory(new List<ISurfaceBuilder>());

            Assert.Throws<DevelopmentException>(() => sut.Generate(typeof(Plateau)));
        }

        [Fact]
        public void WhenBuilderIsFound__BuildMethodShouldBeInvoked()
        {
            var surfaceBuilderMock = new Mock<IPlateauBuilder>();
            var sut = new SurfaceBuilderFactory(new List<ISurfaceBuilder> {surfaceBuilderMock.Object});

            ISurfaceBuilder surfaceBuilder = sut.Generate(typeof(Plateau));

            Assert.NotNull(surfaceBuilder);
            Assert.True(surfaceBuilder is IPlateauBuilder);
        }
    }
}