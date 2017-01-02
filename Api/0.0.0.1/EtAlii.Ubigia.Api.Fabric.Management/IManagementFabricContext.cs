namespace EtAlii.Ubigia.Api.Management
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
