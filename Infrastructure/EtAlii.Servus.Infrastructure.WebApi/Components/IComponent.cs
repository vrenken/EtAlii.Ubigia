namespace EtAlii.Servus.Infrastructure.WebApi
{
    using Owin;

    public interface IComponent
    {
        void Start(IAppBuilder application);
        void Stop();
    }
}