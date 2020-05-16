using System.IO;

namespace MarsRover.Services.StreamSection.StreamReaders
{
    public class BasicStreamReader : IStreamReader
    {
        private StreamReader _streamReader;

        public string ReadLine()
        {
            return _streamReader.ReadLine();
        }

        public void TargetFile(string filePath)
        {
            _streamReader = new StreamReader(filePath);
        }

        public bool EndOfStream()
        {
            return _streamReader.EndOfStream;
        }

        public void Close()
        {
            _streamReader.Close();
        }

        public void Dispose()
        {
            _streamReader?.Dispose();
        }
    }
}