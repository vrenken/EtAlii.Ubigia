namespace EtAlii.Ubigia.Infrastructure.SignalR
{
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.WebApi;
    using Owin;

    public partial class SignalRComponentManager : ISignalRComponentManager
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

        public void Start(object iAppBuilder)
        {
            var application = (IAppBuilder)iAppBuilder;
            foreach (var component in _components)
            {
                component.Start(application);
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