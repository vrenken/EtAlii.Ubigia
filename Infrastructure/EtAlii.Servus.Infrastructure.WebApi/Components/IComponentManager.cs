namespace EtAlii.Servus.Infrastructure.WebApi
{
    using Owin;

    public interface IComponentManager
    {
        void Start(IAppBuilder application);
        void Stop();
    }
}