namespace MarsRover.Services.StreamSection
{
    public interface IFileProcessor
    {
        bool Exists(string fullFilePath);
    }
}