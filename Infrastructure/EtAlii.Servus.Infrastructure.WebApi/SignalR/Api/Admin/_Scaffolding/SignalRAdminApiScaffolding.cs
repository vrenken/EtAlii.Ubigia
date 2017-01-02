namespace EtAlii.Servus.Infrastructure.SignalR
{
    using Microsoft.AspNet.SignalR;
    using SimpleInjector;

    internal class SignalRAdminApiScaffolding : IScaffolding
    {
        private readonly DefaultDependencyResolver _signalRDependencyResolver;

        public SignalRAdminApiScaffolding(DefaultDependencyResolver signalRDependencyResolver)
        {
            _signalRDependencyResolver = signalRDependencyResolver;
        }

        public void Register(Container container)
        {
            container.Register<StorageHub>(Lifestyle.Singleton);
            container.Register<SpaceHub>(Lifestyle.Singleton);
            container.Register<AccountHub>(Lifestyle.Singleton);

            _signalRDependencyResolver.Register(typeof(StorageHub), container.GetInstance<StorageHub>);
            _signalRDependencyResolver.Register(typeof(SpaceHub), container.GetInstance<SpaceHub>);
            _signalRDependencyResolver.Register(typeof(AccountHub), container.GetInstance<AccountHub>);
        }
    }
}