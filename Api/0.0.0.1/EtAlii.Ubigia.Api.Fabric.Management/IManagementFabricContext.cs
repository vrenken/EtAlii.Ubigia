namespace EtAlii.Ubigia.Api.Fabric.Management
{
    using EtAlii.Ubigia.Api.Transport;

    public interface IManagementFabricContext
    {
        Storage Storage { get; }
        IStorageContext Storages { get; }
        IAccountContext Accounts { get; }
        ISpaceContext Spaces { get; }
    }
}
