namespace EtAlii.Ubigia.Infrastructure.Transport.WebApi
{
    using System;
    using Owin;

    public interface IWebApiComponent
    {
        void Start(IAppBuilder application);
        void Stop();

        bool TryGetService(Type serviceType, out object serviceInstance);
    }
}