namespace EtAlii.Servus.Provisioning
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Management;

    public interface IProviderContext
    {
        IDataContext SystemDataContext { get; }

        IManagementConnection ManagementConnection { get; }

        IDataContext CreateDataContext(string accountName, string spaceName);
        IDataContext CreateDataContext(Space space);
    }
}