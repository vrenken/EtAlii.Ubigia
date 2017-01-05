namespace EtAlii.Ubigia.Infrastructure.Transport
{
    public interface IApplicationManager
    {
        void Start(IComponentManager[] componentManagers);
        void Stop(IComponentManager[] componentManagers);
    }
}