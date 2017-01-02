namespace EtAlii.Servus.Api.Transport
{
    using EtAlii.xTechnology.MicroContainer;

    internal class DataConnectionBaseScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            // Added from DataConnectionScaffolding.
            container.Register<IConnectionStatusProvider, ConnectionStatusProvider>();
            container.Register<IDataConnection, DataConnection>();
            container.RegisterInitializer<IDataConnection>(connection =>
            {
                container.GetInstance<IDataConnectionContext>().Initialize(connection);
                container.GetInstance<IConnectionStatusProvider>().Initialize(connection);

                var transport = container.GetInstance<ITransport>();
                transport.Initialize(container.GetInstance<IClientContext>());

            });

            container.Register<IAuthenticationContext, AuthenticationContext>();
            container.Register<IEntryContext, EntryContext>();
            container.Register<IRootContext, RootContext>();
            container.Register<IContentContext, ContentContext>();
            container.Register<IPropertyContext, PropertyContext>();
        }
    }
}