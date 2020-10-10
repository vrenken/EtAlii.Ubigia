namespace EtAlii.xTechnology.Hosting.Tests.Provisioning.Grpc
{
    public interface ISystemCommandsFactory
    {
        ICommand[] Create(ISystem system);
    }
}