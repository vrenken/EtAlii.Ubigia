namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using Owin;

    public interface IComponent
    {
        void Start(IAppBuilder application);
        void Stop();

        bool TryGetService(Type serviceType, out object serviceInstance);
        //bool TryGetServices(Type serviceType, IEnumerable<object> serviceInstances);
    }
}