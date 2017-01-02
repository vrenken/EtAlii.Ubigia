namespace EtAlii.Ubigia.PowerShell
{
    using System;
    using EtAlii.Ubigia.Api.Management;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using SimpleInjector;

    public class PowerShellClientFactory
    {
        public PowerShellClientFactory()
        {
        }

        public IPowerShellClient Create<T>(IInfrastructureClient infrastructureClient = null)
            where T : class, IPowerShellClient
        {
            var container = new Container();
            container.ResolveUnregisteredType += (sender, args) => { throw new InvalidOperationException("Unregistered type found: " + args.UnregisteredServiceType.Name); };

            container.Register<IPowerShellClient, T>(Lifestyle.Singleton);
            RegisterStructure(container, infrastructureClient);

            return container.GetInstance<IPowerShellClient>();
        }

        private void RegisterStructure(Container container, IInfrastructureClient infrastructureClient)
        {
            if(infrastructureClient != null)
            {
                container.Register<IInfrastructureClient>(() => infrastructureClient, Lifestyle.Singleton);
            }
            else
            {
                container.Register<ISerializer>(() => new SerializerFactory().Create(), Lifestyle.Singleton);
                container.Register<IInfrastructureClient, DefaultInfrastructureClient>(Lifestyle.Singleton);
                container.Register<IHttpClientFactory, DefaultHttpClientFactory>(Lifestyle.Singleton);
            }

            container.Register<IAddressFactory, AddressFactory>(Lifestyle.Singleton);
            container.Register<IStorageResolver, StorageResolver>(Lifestyle.Singleton);
            container.Register<IAccountResolver, AccountResolver>(Lifestyle.Singleton);
            container.Register<ISpaceResolver, SpaceResolver>(Lifestyle.Singleton);
            container.Register<IRootResolver, RootResolver>(Lifestyle.Singleton);
            container.Register<IEntryResolver, EntryResolver>(Lifestyle.Singleton);
        }
    }
}
