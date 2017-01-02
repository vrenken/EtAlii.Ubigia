namespace EtAlii.Ubigia.PowerShell
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Management;
    using EtAlii.Ubigia.Api.Transport;

    public interface IPowerShellClient
    {
        IStorageResolver StorageResolver { get; }
        IEntryResolver EntryResolver { get; }
        ISpaceResolver SpaceResolver { get; }
        IAccountResolver AccountResolver { get; }
        IRootResolver RootResolver { get; }

        IManagementConnection ManagementConnection { get; }
        IFabricContext Fabric { get; set; }
        IInfrastructureClient Client { get; }

        Task OpenManagementConnection(string address, string accountName, string password);
    }
}
