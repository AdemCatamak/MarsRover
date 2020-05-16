using System;
using MarsRover.Services.InputProviderSection.Exceptions;
using MarsRover.Services.StreamSection;
using MarsRover.Services.StreamSection.Exceptions;

namespace MarsRover.Services.InputProviderSection.InputProviders
{
    public class FileInputProvider : IFileInputProvider, IDisposable
    {
        private readonly IStreamReader _streamReader;
        private readonly IFileProcessor _fileProcessor;

        public FileInputProvider(IStreamReader streamReader, IFileProcessor fileProcessor)
        {
            _streamReader = streamReader;
            _fileProcessor = fileProcessor;
        }

        public Input Provide(string arg)
        {
            if (!_fileProcessor.Exists(arg))
            {
                throw new FileNotFoundException(arg);
            }

            var input = new Input();

            _streamReader.TargetFile(arg);

            if (_streamReader.EndOfStream())
            {
                throw new InputNotValidException("File should not be empty");
            }

            string surfaceParameter = _streamReader.ReadLine();
            input.SurfaceParameter = surfaceParameter;

            while (!_streamReader.EndOfStream())
            {
                string vehicleParameter = _streamReader.ReadLine();
                if(string.IsNullOrEmpty(vehicleParameter)) break;
                
                if (_streamReader.EndOfStream())
                {
                    throw new InputNotValidException("Each vehicle should contains at least one move command");
                }

                string moveCommandsParameter = _streamReader.ReadLine();

                var vehicleAndMoveCommandsPair = new Tuple<string, string>(vehicleParameter, moveCommandsParameter);
                input.VehicleAndCommandsParameterList.Add(vehicleAndMoveCommandsPair);
            }

            return input;
        }

        public string FormatInfo
        {
            get => "Filename";
        }

        public void Dispose()
        {
            _streamReader?.Dispose();
        }
    }
}