using System;
using System.Collections.Generic;
using MarsRover.Exceptions;
using MarsRover.Services.InputProviderSection;
using MarsRover.Services.InputProviderSection.Exceptions;
using MarsRover.Services.InputProviderSection.InputProviderFactories;
using MarsRover.Services.InputProviderSection.InputProviders;
using Moq;
using Xunit;

namespace MarsRover.ServicesTests
{
    public class InputProviderFactory_GenerateTests
    {
        [Fact]
        public void WhenInputProvidersIsNull_DevelopmentExceptionOccurs()
        {
            Assert.Throws<DevelopmentException>(() => new InputProviderFactory(null));
        }

        [Fact]
        public void WhenInputProviderTypeIsNotValid__InputProviderTypeIsNotValidExceptionOccurs()
        {
            var sut = new InputProviderFactory(new List<IInputProvider>());

            Assert.Throws<InputProviderNotValidException>(() => sut.Generate(0));
        }

        [Fact]
        public void WhenInputProviderCollectionDoesNotContainsAnyItem__DevelopmentExceptionOccurs()
        {
            var sut = new InputProviderFactory(new List<IInputProvider>());

            Assert.Throws<DevelopmentException>(() => sut.Generate(InputProviderTypes.Console));
        }

        [Theory]
        [InlineData(InputProviderTypes.Console, typeof(IConsoleInputProvider))]
        [InlineData(InputProviderTypes.File, typeof(IFileInputProvider))]
        public void WhenParametersAreValid_And_InputProviderIsFound__ResponseShouldNotBeNull(InputProviderTypes inputProviderType, Type expectedType)
        {
            var sut = new InputProviderFactory(new List<IInputProvider>
                                               {
                                                   new Mock<IFileInputProvider>().Object,
                                                   new Mock<IConsoleInputProvider>().Object
                                               });

            IInputProvider inputProvider = sut.Generate(inputProviderType);

            Assert.NotNull(inputProvider);
            Assert.True(expectedType.IsInstanceOfType(inputProvider));
        }
    }
}