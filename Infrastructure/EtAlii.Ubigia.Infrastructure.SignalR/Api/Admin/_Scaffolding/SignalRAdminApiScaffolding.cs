namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.SignalR
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.AspNet.SignalR;
    using EtAlii.xTechnology.MicroContainer;

    public class SignalRAdminApiScaffolding : IScaffolding
    {
        private readonly IDependencyResolver _signalRDependencyResolver;

        public SignalRAdminApiScaffolding(IDependencyResolver signalRDependencyResolver)
        {
            _signalRDependencyResolver = signalRDependencyResolver;
        }

        public void Register(Container container)
        {
            _signalRDependencyResolver.Register(typeof(StorageHub), () => new StorageHub(container.GetInstance<IStorageRepository>()));
            _signalRDependencyResolver.Register(typeof(SpaceHub), () => new SpaceHub(container.GetInstance<ISpaceRepository>()));
            _signalRDependencyResolver.Register(typeof(AccountHub), () => new AccountHub(container.GetInstance<IAccountRepository>()));

            _signalRDependencyResolver.Register(typeof(IStorageRepository), container.GetInstance<IStorageRepository>);
            _signalRDependencyResolver.Register(typeof(IAccountRepository), container.GetInstance<IAccountRepository>);
            _signalRDependencyResolver.Register(typeof(ISpaceRepository), container.GetInstance<ISpaceRepository>);
        }
    }
}