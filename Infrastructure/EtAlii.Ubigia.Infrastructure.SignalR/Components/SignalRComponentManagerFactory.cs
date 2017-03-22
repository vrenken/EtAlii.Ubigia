namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.SignalR
{
    using System;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.AspNet.SignalR;

    public class SignalRComponentManagerFactory
    {
        public ISignalRComponentManager Create(IDependencyResolver dependencyResolver, Func<Container, object>[] components)
        {
            var container = new Container();
            container.Register<ISignalRComponentManager, SignalRComponentManager>();
            container.Register(() => dependencyResolver);

            container.Register<ISignalRUserApiComponent, SignalRUserApiComponent>();
            container.Register<ISignalRAdminApiComponent, SignalRAdminApiComponent>();

            return container.GetInstance<ISignalRComponentManager>();
        }
    }
}