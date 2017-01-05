namespace EtAlii.Ubigia.Infrastructure.Transport.WebApi
{
    public interface IApplicationManager
    {
        void Start(IComponentManager[] componentManagers);
        void Stop(IComponentManager[] componentManagers);
    }
}