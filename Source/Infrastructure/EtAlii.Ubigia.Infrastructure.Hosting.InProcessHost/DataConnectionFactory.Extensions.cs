namespace EtAlii.Ubigia.Infrastructure.Hosting.Local
{
    using EtAlii.Ubigia;
    using EtAlii.xTechnology.MicroContainer;

    public static class DataConnectionFactoryExtensions
    {
        public static ILocalDataConnection CreateLocal(this DataConnectionFactory factory)
        {
            var container = new Container();

            container.RegisterSingle<IDataTransport, LocalDataTransport>();
            container.RegisterSingle<INotificationTransport, LocalNotificationTransport>();
            container.RegisterSingle<ILocalDataConnection>(() => new LocalDataConnection(container.GetInstance<IDataConnection>()));

            DataConnectionFactory.RegisterStructure(container);
            DataConnectionFactory.RegisterDataClients<LocalEntryDataClient, LocalRootDataClient, LocalContentDataClient>(container);
            DataConnectionFactory.RegisterNotificationClients<LocalEntryNotificationClient, LocalRootNotificationClient, LocalContentNotificationClient>(container);

            return container.GetInstance<ILocalDataConnection>();
        }
    }
}
