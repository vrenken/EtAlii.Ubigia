namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.SignalR
{
    using EtAlii.xTechnology.Hosting.Owin;
    using System.Linq;
    using global::Owin;

    public class SignalRComponentManager : ISignalRComponentManager
    {
        private readonly IComponent[] _components;

        public SignalRComponentManager(
            ISignalRUserApiComponent signalRUserApiComponent,
            ISignalRAdminApiComponent signalRAdminApiComponent)
        {
            _components = new IComponent[]
            {
                signalRUserApiComponent,
                signalRAdminApiComponent,
            };
        }

        public void Start(IAppBuilder applicationBuilder)
        {
            foreach (var component in _components)
            {
                component.Start(applicationBuilder);
            }
        }

        public void Stop()
        {
            foreach (var component in _components.Reverse())
            {
                component.Stop();
            }
        }
    }
}