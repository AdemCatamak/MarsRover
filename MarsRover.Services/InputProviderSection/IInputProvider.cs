namespace MarsRover.Services.InputProviderSection
{
    public interface IInputProvider
    {
        Input Provide(string arg);
        string FormatInfo { get; }
    }
}