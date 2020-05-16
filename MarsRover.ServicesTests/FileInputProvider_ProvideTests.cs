using MarsRover.Services.InputProviderSection.Exceptions;
using MarsRover.Services.InputProviderSection.InputProviders;
using MarsRover.Services.StreamSection;
using MarsRover.Services.StreamSection.Exceptions;
using Moq;
using Xunit;

namespace MarsRover.ServicesTests
{
    public class FileInputProvider_ProvideTests
    {
        private FileInputProvider _sut;

        private readonly Mock<IStreamReader> _streamReaderMock;
        private readonly Mock<IFileProcessor> _fileProcessorMock;

        public FileInputProvider_ProvideTests()
        {
            _streamReaderMock = new Mock<IStreamReader>();
            _streamReaderMock.Setup(reader => reader.ReadLine())
                             .Returns("line");
            _fileProcessorMock = new Mock<IFileProcessor>();
            _fileProcessorMock.Setup(processor => processor.Exists(It.IsAny<string>()))
                              .Returns(true);


            _sut = new FileInputProvider(_streamReaderMock.Object, _fileProcessorMock.Object);
        }

        [Fact]
        public void WhenFileNotExist__FileNotFoundExceptionOccurs()
        {
            _fileProcessorMock.Setup(processor => processor.Exists(It.IsAny<string>()))
                              .Returns(false);

            Assert.Throws<FileNotFoundException>(() => _sut.Provide("some-file-name"));
        }

        [Fact]
        public void WhenFileDoesNotContainsAnyLine__InputNotValidExceptionOccurs()
        {
            _streamReaderMock.Setup(reader => reader.EndOfStream())
                             .Returns(true);

            Assert.Throws<InputNotValidException>(() => _sut.Provide("some-file-name"));
        }

        [Fact]
        public void WhenFileDoesNotContainsAnyVehicle__InputNotValidExceptionOccurs()
        {
            var endOfStreamCallCount = 0;
            _streamReaderMock.Setup(reader => reader.EndOfStream())
                             .Returns(() =>
                                      {
                                          endOfStreamCallCount++;
                                          return endOfStreamCallCount >= 1;
                                      });

            Assert.Throws<InputNotValidException>(() => _sut.Provide("some-file-name"));
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        public void WhenEveryVehicleDoesNotContainCommands__InputNotValidExceptionOccurs(int untilEndOfFile)
        {
            var endOfStreamCallCount = 0;
            _streamReaderMock.Setup(reader => reader.EndOfStream())
                             .Returns(() =>
                                      {
                                          endOfStreamCallCount++;
                                          return !(endOfStreamCallCount < untilEndOfFile);
                                      });

            Assert.Throws<InputNotValidException>(() => _sut.Provide("some-file-name"));
        }

        [Theory]
        [InlineData(4, 1)]
        [InlineData(6, 2)]
        public void WhenEveryVehicleContainsCommands__InputNotValidExceptionOccurs(int untilEndOfFile, int expectedVehiclCount)
        {
            var endOfStreamCallCount = 0;
            _streamReaderMock.Setup(reader => reader.EndOfStream())
                             .Returns(() =>
                                      {
                                          endOfStreamCallCount++;
                                          return !(endOfStreamCallCount < untilEndOfFile);
                                      });

            var input = _sut.Provide("some-file-name");

            Assert.NotNull(input);

            Assert.Equal(expectedVehiclCount, input.VehicleAndCommandsParameterList.Count);
        }
    }
}