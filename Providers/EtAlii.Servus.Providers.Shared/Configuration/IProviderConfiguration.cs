namespace EtAlii.Servus.Provisioning
{
    using System;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.xTechnology.Logging;

    public interface IProviderConfiguration
    {
        IDataContext SystemDataContext { get; }

        IManagementConnection ManagementConnection { get; }

        IProviderExtension[] Extensions { get; }

        IProviderFactory Factory { get; }
        ILogFactory LogFactory { get; }

        IProviderConfiguration Use(IProviderFactory factory);
        IProviderConfiguration Use(IProviderExtension[] extensions);
        IProviderConfiguration Use(ILogFactory logFactory);
        IProviderConfiguration Use(IManagementConnection managementConnection);
        IProviderConfiguration Use(IDataContext systemDataContext);

        IProviderConfiguration Use(Func<IDataConnection, IDataContext> dataConnectionFactory);
        IDataContext CreateDataContext(IDataConnection connection);
    }
}