namespace EtAlii.Ubigia.PowerShell
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.WebApi;

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
