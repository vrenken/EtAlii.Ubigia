namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    using EtAlii.xTechnology.Hosting;

    public interface ISystemCommandsFactory
    {
        ICommand[] Create(ISystem system);
    }
}