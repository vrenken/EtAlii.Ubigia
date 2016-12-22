namespace EtAlii.Servus.Api.Management
{
    using EtAlii.Servus.Api.Transport;

    public interface IManagementFabricContext
    {
        Storage Storage { get; }
        IStorageContext Storages { get; }
        IAccountContext Accounts { get; }
        ISpaceContext Spaces { get; }
    }
}
