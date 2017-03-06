namespace EtAlii.Ubigia.Provisioning
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport.Management;

    public interface IProviderContext
    {
        IDataContext SystemDataContext { get; }

        IManagementConnection ManagementConnection { get; }

        IDataContext CreateDataContext(string accountName, string spaceName);
        IDataContext CreateDataContext(Space space);
    }
}