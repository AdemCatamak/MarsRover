using System;

namespace MarsRover.Services.StreamSection
{
    public interface IStreamReader : IDisposable
    {
        string ReadLine();
        void TargetFile(string filePath);
        bool EndOfStream();
        void Close();
    }
}