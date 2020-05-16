namespace MarsRover.Services.InputProviderSection
{
    public interface IInputProviderFactory
    {
        IInputProvider Generate(InputProviderTypes inputProviderType);
    }
}