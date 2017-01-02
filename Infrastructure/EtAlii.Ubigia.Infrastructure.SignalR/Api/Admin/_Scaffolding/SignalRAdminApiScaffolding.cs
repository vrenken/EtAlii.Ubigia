namespace EtAlii.Ubigia.Infrastructure.SignalR
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.AspNet.SignalR;
    using SimpleInjector;

    public class SignalRAdminApiScaffolding : IScaffolding
    {
        private readonly IDependencyResolver _signalRDependencyResolver;

        public SignalRAdminApiScaffolding(IDependencyResolver signalRDependencyResolver)
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