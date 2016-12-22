namespace EtAlii.Servus.Infrastructure.WebApi
{
    public interface IApplicationManager
    {
        void Start(IComponentManager[] componentManagers);
        void Stop(IComponentManager[] componentManagers);
    }
}