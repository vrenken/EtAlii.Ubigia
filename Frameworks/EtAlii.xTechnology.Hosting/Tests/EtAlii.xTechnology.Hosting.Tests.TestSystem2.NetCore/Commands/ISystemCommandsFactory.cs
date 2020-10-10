namespace EtAlii.xTechnology.Hosting.Tests.Provisioning.NetCore
{
    public interface ISystemCommandsFactory
    {
        ICommand[] Create(ISystem system);
    }
}