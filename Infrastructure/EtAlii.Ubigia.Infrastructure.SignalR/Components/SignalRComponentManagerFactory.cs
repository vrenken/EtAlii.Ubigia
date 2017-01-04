namespace EtAlii.Ubigia.Infrastructure.SignalR
{
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.AspNet.SignalR;

    public class SignalRComponentManagerFactory
    {
        public ISignalRComponentManager Create(IDependencyResolver dependencyResolver, object[] components)
        {
            var container = new Container();
            container.Register<ISignalRComponentManager, SignalRComponentManager>();
            container.Register<IDependencyResolver>(() => dependencyResolver);

            container.Register<ISignalRUserApiComponent, SignalRUserApiComponent>();
            container.Register<ISignalRAdminApiComponent, SignalRAdminApiComponent>();

            return container.GetInstance<ISignalRComponentManager>();
        }
    }
}