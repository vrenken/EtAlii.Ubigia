namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.xTechnology.Hosting;

    public interface ISystemCommandsFactory
    {
        ICommand[] Create(ISystem system);
    }
}