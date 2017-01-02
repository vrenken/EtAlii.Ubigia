namespace EtAlii.Servus.Hosting
{
    using Owin;

    public interface IComponentManager
    {
        void Start(IAppBuilder application);
        void Stop();
    }
}