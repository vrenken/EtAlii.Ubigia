namespace EtAlii.Ubigia.Infrastructure.WebApi
{
    public interface IApplicationManager
    {
        void Start(IComponentManager[] componentManagers);
        void Stop(IComponentManager[] componentManagers);
    }
}