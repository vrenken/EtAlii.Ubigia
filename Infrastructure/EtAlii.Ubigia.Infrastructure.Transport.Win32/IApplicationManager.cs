namespace EtAlii.Ubigia.Infrastructure.Transport.Owin
{
    public interface IApplicationManager
    {
        void Start(IComponentManager[] componentManagers);
        void Stop(IComponentManager[] componentManagers);
    }
}