namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi
{
    using System;
    using global::Owin;

    public interface IWebApiComponent
    {
        void Start(IAppBuilder application);
        void Stop();

        bool TryGetService(Type serviceType, out object serviceInstance);
    }
}