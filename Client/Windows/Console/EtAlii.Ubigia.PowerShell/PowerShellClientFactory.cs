namespace EtAlii.Ubigia.PowerShell
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.xTechnology.MicroContainer;

    public class PowerShellClientFactory
    {
        public IPowerShellClient Create<T>(IInfrastructureClient infrastructureClient = null)
            where T : class, IPowerShellClient
        {
            var container = new Container();
            //container.ResolveUnregisteredType += (sender, args) => { throw new InvalidOperationException("Unregistered type found: " + args.UnregisteredServiceType.Name); }

            container.Register<IPowerShellClient, T>();
            RegisterStructure(container, infrastructureClient);

            return container.GetInstance<IPowerShellClient>();
        }

        private void RegisterStructure(Container container, IInfrastructureClient infrastructureClient)
        {
            if(infrastructureClient != null)
            {
                container.Register(() => infrastructureClient);
            }
            else
            {
                container.Register(() => new SerializerFactory().Create());
                container.Register<IInfrastructureClient, DefaultInfrastructureClient>();
                container.Register<IHttpClientFactory, DefaultHttpClientFactory>();
            }

            container.Register<IAddressFactory, AddressFactory>();
            container.Register<IStorageResolver, StorageResolver>();
            container.Register<IAccountResolver, AccountResolver>();
            container.Register<ISpaceResolver, SpaceResolver>();
            container.Register<IRootResolver, RootResolver>();
            container.Register<IEntryResolver, EntryResolver>();
        }
    }
}
