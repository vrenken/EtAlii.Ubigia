namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.NetCore
{
    public interface ISystemCommandsFactory
    {
        ICommand[] Create(ISystem system);
    }
}