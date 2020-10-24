namespace EtAlii.xTechnology.Hosting
{
    using System;
    using Microsoft.Extensions.Hosting;

    public interface IComponent
    {
        void Start(IHostBuilder hostBuilder);
        void Stop();

        bool TryGetService(Type serviceType, out object serviceInstance);
    }
}