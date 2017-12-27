namespace EtAlii.Ubigia.Infrastructure.Hosting.AspNetCore
{
    using EtAlii.xTechnology.Hosting;

    public interface ISystemCommandsFactory
    {
        ICommand[] Create(ISystem system);
    }
}