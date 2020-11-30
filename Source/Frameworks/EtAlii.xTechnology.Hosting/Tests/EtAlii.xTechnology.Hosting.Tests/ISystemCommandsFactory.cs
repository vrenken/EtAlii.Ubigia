namespace EtAlii.xTechnology.Hosting.Tests
{
    public interface ISystemCommandsFactory
    {
        ICommand[] Create(ISystem system);
    }
}