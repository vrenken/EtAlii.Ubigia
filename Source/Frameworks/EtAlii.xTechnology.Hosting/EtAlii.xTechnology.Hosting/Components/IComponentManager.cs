namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Hosting;

    public interface IComponentManager
    {
        void Start(IHostBuilder hostBuilder);
        void Stop();
    }
}